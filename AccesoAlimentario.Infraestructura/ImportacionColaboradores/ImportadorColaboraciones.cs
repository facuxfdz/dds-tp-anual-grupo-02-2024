namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores
{
    public class ImportadorColaboraciones
    {
        private readonly FormaImportacion _formaImportacion;

        public ImportadorColaboraciones(FormaImportacion formaImportacion)
        {
            _formaImportacion = formaImportacion;
        }

        public void Importar(Stream fileStream)
        {
            var colaboradores = _formaImportacion.ImportarColaboradores(fileStream);
            // TODO Buscar colaboradores existentes
            // TODO Guardar colaboradores nuevos y colaboraciones realizadas
        }
    }
}