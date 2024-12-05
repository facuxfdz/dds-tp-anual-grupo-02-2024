import os
import requests
import time
import logging
from flask import Flask, request, jsonify
from flask_restx import Api, Resource, fields, Namespace

# Configuración inicial
app = Flask(__name__)
api = Api(
    app,
    title="Recomendaciones de Colaboradores",
    version="1.0",
    description="API para filtrar colaboradores",
    doc="/docs",  # Endpoint para documentación OpenAPI
)
LOGGER = logging.getLogger("recomendaciones_service")
LOGGER.setLevel(logging.DEBUG)

# Configuración del logger
handler = logging.StreamHandler()
formatter = logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s")
handler.setFormatter(formatter)
LOGGER.addHandler(handler)

# URL de la API de colaboradores (variable de entorno)
COLABORADORES_API_URL = os.getenv("COLABORADORES_API_URL", "http://localhost:5000/api/colaboradores/externos")

# Crear un namespace personalizado
recomendaciones_ns = Namespace("recomendaciones", description="Operaciones relacionadas con recomendaciones")

# Esquemas para Swagger
colaborador_response_model = recomendaciones_ns.model(
    "ColaboradorResponse",
    {
        "id": fields.String(description="ID del colaborador"),
        "nombre": fields.String(description="Nombre del colaborador"),
        "puntos": fields.Integer(description="Puntos del colaborador"),
        "donaciones_ultimo_mes": fields.Integer(description="Cantidad de donaciones de viandas en el último mes"),
    },
)

recomendaciones_request_model = recomendaciones_ns.model(
    "RecomendacionesRequest",
    {
        "puntos_minimos": fields.Integer(required=True, description="Cantidad mínima de puntos"),
        "donaciones_viandas_minimas": fields.Integer(required=True, description="Donaciones de viandas mínimas"),
        "cantidad_de_colaboradores": fields.Integer(required=True, description="Cantidad máxima de colaboradores"),
    },
)


# Función para convertir fechas en formato ISO8601 a timestamp
def parse_iso8601_to_timestamp(iso8601_str):
    try:
        return time.mktime(time.strptime(iso8601_str, "%Y-%m-%dT%H:%M:%S"))
    except ValueError as e:
        LOGGER.error(f"Error al convertir fecha: {iso8601_str}, error: {str(e)}")
        return None


# Endpoint de recomendaciones
@recomendaciones_ns.route("/recomendaciones")
class RecomendacionesResource(Resource):
    @recomendaciones_ns.expect(recomendaciones_request_model, validate=True)
    @recomendaciones_ns.marshal_list_with(colaborador_response_model)
    def get(self):
        """Obtener colaboradores recomendados desde la API"""
        try:
            data = request.json
            puntos_minimos = data["puntos_minimos"]
            donaciones_viandas_minimas = data["donaciones_viandas_minimas"]
            cantidad_de_colaboradores = data["cantidad_de_colaboradores"]

            # Consultar colaboradores desde la API
            response = requests.get(COLABORADORES_API_URL, timeout=10)
            LOGGER.info(f"Respuesta de la API de colaboradores: {response.status_code}")
            if response.status_code != 200:
                LOGGER.error("No se pudo obtener la lista de colaboradores")
                return {"error": "Error al obtener colaboradores"}, 500

            colaboradores = response.json()

            # Calcular timestamp del último mes
            ahora = time.time()
            hace_un_mes = ahora - (30 * 24 * 60 * 60)

            # Filtrar colaboradores
            colaboradores_validos = []
            for colaborador in colaboradores:
                contribuciones = colaborador.get("contribuciones", [])
                donaciones_viandas = [
                    c
                    for c in contribuciones
                    if c.get("tipo") == "vianda" and parse_iso8601_to_timestamp(c.get("fecha_contribucion", "")) >= hace_un_mes
                ]
                if colaborador["puntos"] >= puntos_minimos and len(donaciones_viandas) >= donaciones_viandas_minimas:
                    colaboradores_validos.append(
                        {
                            "id": colaborador["id"],
                            "nombre": colaborador["nombre"],
                            "puntos": colaborador["puntos"],
                            "donaciones_ultimo_mes": len(donaciones_viandas),
                        }
                    )

            # Ordenar y limitar los resultados
            colaboradores_validos = sorted(colaboradores_validos, key=lambda x: x["puntos"], reverse=True)
            return colaboradores_validos[:cantidad_de_colaboradores], 200

        except KeyError as e:
            LOGGER.error(f"Parámetro faltante: {str(e)}")
            return {"error": f"Parámetro faltante: {str(e)}"}, 400
        except requests.RequestException as e:
            LOGGER.error(f"Error al conectar con la API de colaboradores: {str(e)}")
            return {"error": "No se pudo conectar con la API de colaboradores"}, 500
        except Exception as e:
            LOGGER.exception("Error inesperado")
            return {"error": "Error interno del servidor"}, 500

# Registrar el namespace personalizado
api.add_namespace(recomendaciones_ns)

if __name__ == "__main__":
    app.run(debug=True)