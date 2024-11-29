"use client";
import {Box,CircularProgress} from "@mui/material";
import {PremioCard} from "@/app/admin/premios/PremioCard";
import {useGetPremiosQuery} from "@/redux/services/premiosApi";
import {FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import Grid from "@mui/material/Grid2";
import React, {useState} from "react";

interface premioItem {
    id: string,
    nombre: string,
    puntos: number
    imagen: string,
    reclamadoPor: string,
    fechaReclamo: string,
    categoria: CategoriaPremio
}

enum CategoriaPremio{
    Gastronomia,
    Electronica,
    ArticulosHogar,
    Otros
}

export default function PremiosPage() {
    const [categoria, setCategoria] = useState('Todos');
    const [puntosMaximos, setPuntosMaximos] = useState(0);
    const [nombre, setNombre] = useState('');
    const {data, isError, isLoading} = useGetPremiosQuery();
    
    if(isLoading){
        return <CircularProgress />;
    }
    
    if(isError || !data){
        return <Box>Error: Ha ocurrido un error al cargar los datos</Box>;
    }


    return (
        <>
            <Grid container spacing={3} mb={3}>
                <Grid size={4} key={"categoria"}>
                    <FormControl fullWidth>
                        <InputLabel id="categoria-label">Categoria</InputLabel>
                        <Select
                            labelId="categoria-label"
                            value={categoria}
                            onChange={(e) => setCategoria(e.target.value)}
                            label="Categoria"
                        >
                            <MenuItem value="Todos">Todos</MenuItem>
                            <MenuItem value="Gastronomia">Gastronomia</MenuItem>
                            <MenuItem value="Electronica">Electronica</MenuItem>
                            <MenuItem value="ArticulosHogar">Articulos Hogar</MenuItem>
                            <MenuItem value="Otros">Otros</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid size={4} key={"puntos"}>
                    <FormControl fullWidth>
                        <TextField
                            fullWidth
                            label={"Puntos máximos"}
                            value={puntosMaximos}
                            onChange={(e) => setPuntosMaximos(Number(e.target.value))}
                            variant="outlined"
                            type={"number"}
                        />
                    </FormControl>
                </Grid>
                <Grid size={4} key={"nombre"}>
                    <FormControl fullWidth>
                        <TextField
                            fullWidth
                            label={"Nombre"}
                            value={nombre}
                            onChange={(e) => setNombre(e.target.value)}
                            variant="outlined"
                        />
                    </FormControl>
                </Grid>
            </Grid>
            <Grid container spacing={3}>
                {
                    data
                        .filter((premio: premioItem) => categoria === 'Todos' ? true : premio.categoria.toString() === categoria)
                        .filter((premio: premioItem) => puntosMaximos > 0 ? premio.puntos <= puntosMaximos : true)
                        .filter((premio: premioItem) => nombre.length > 0 ? premio.nombre.includes(nombre) : true)
                        .map((premio: premioItem) => (
                            <PremioCard
                                id= {parseInt(premio.id)}
                                nombre={premio.nombre}
                                descripcion="Prueba Descripcion"
                                puntos={premio.puntos}
                                imagen={premio.imagen}
                                categoria={premio.categoria.toString()}
                                key={premio.id}
                            />
                        ))
                }
            </Grid>
        </>
    );
}