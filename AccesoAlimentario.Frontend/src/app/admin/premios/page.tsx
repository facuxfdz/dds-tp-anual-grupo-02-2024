"use client";
import {PremioCard} from "@/app/admin/premios/PremioCard";
import {Box, CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import Grid from "@mui/material/Grid2";
import React, {useState} from "react";
import {useGetPremiosQuery} from "@redux/services/premiosApi";
import {useTheme} from "@mui/material/styles";
import {IObtenerPremioResponse} from "@models/responses/premios/iObtenerPremioResponse";

/*interface premioItem {
    id: number;
    nombre: string;
    descripcion: string;
    puntos: number;
    imagen: string;
    categoria: string;
}

const premios: premioItem[] = [
    {
        id: 1,
        nombre: 'Premio 1',
        descripcion: 'Descripción del premio 1',
        puntos: 100,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Gastronomia'
    },
    {
        id: 2,
        nombre: 'Premio 2',
        descripcion: 'Descripción del premio 2',
        puntos: 200,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Electronica'
    },
    {
        id: 3,
        nombre: 'Premio 3',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'ArticulosHogar'
    },
    {
        id: 4,
        nombre: 'Premio 4',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Otros'
    },
    {
        id: 5,
        nombre: 'Premio 5',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Electronica'
    },
    {
        id: 6,
        nombre: 'Premio 6',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Gastronomia'
    },
    {
        id: 7,
        nombre: 'Premio 7',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Otros'
    },
    {
        id: 8,
        nombre: 'Premio 8',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Otros'
    },
    {
        id: 9,
        nombre: 'Premio 9',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Gastronomia'
    },
    {
        id: 10,
        nombre: 'Premio 10',
        descripcion: 'Descripción del premio 3',
        puntos: 300,
        imagen: "https://enriquetomas.com/cdn/shop/articles/donde-comprar-jamon-iberico-de-bellota.jpg",
        categoria: 'Electronica'
    }
];*/

export default function PremiosPage() {
    // Es necesario o dentro de lo que debería tomar del response de la API?
    const [categoria, setCategoria] = useState('Todos');
    const [puntosMaximos, setPuntosMaximos] = useState(0);
    const [nombre, setNombre] = useState('');
    
    const theme = useTheme();
    const {data, isError, isLoading} = useGetPremiosQuery({
        categoria : undefined,
        puntosNecesarios: puntosMaximos > 0 ? puntosMaximos : undefined,
        nombre: nombre.length > 0 ? nombre : undefined,
    });
    
    if (isLoading){
        return <CircularProgress />
    }
    
    if (isError || !data){
        return <Box>Error: Ha ocurrido un error al cargar los datos</Box>
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
                        .filter((premio: IObtenerPremioResponse) => categoria === 'Todos' ? true : premio.categoria === categoria)
                        .filter((premio: IObtenerPremioResponse) => puntosMaximos > 0 ? premio.puntosNecesarios <= puntosMaximos : true)
                        .filter((premio: IObtenerPremioResponse) => nombre.length > 0 ? premio.nombre.includes(nombre) : true)
                        .map((premio: IObtenerPremioResponse) => (
                            <PremioCard
                                id={premio.id}
                                nombre={premio.nombre}
                                descripcion={premio.descripcion}
                                puntos={premio.puntosNecesarios}
                                imagen={premio.imagen}
                                categoria={premio.categoria}
                                key={premio.id}
                            />
                        ))
                }
            </Grid>
        </>
    );
}