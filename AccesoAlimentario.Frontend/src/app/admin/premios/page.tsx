"use client";
import {PremioCard} from "@/app/admin/premios/PremioCard";
import {CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import Grid from "@mui/material/Grid2";
import React, {useEffect, useState} from "react";
import {useLazyGetPremiosQuery} from "@redux/services/contribucionesApi";
import {TipoRubro} from "@models/enums/tipoRubro";

export default function PremiosPage() {
    const [rubro, setRubro] = useState<TipoRubro | string>("");
    const [puntosNecesarios, setPuntosNecesarios] = useState<number | null>(null);
    const [nombre, setNombre] = useState('');
    const [
        getPremios,
        {data: premiosData, isLoading: premiosIsLoading}
    ] = useLazyGetPremiosQuery();

    useEffect(() => {
        getPremios({
            nombre,
            puntosNecesarios: puntosNecesarios === null ? undefined : puntosNecesarios,
            rubro: rubro === "" ? undefined : rubro
        });
    }, [getPremios, nombre, puntosNecesarios, rubro]);

    return (
        <>
            <Grid container spacing={3} mb={3}>
                <Grid size={4} key={"categoria"}>
                    <FormControl fullWidth>
                        <InputLabel id="categoria-label">Categoria</InputLabel>
                        <Select
                            labelId="categoria-label"
                            value={rubro}
                            onChange={(e) => setRubro(e.target.value)}
                            label="Categoria"
                        >
                            <MenuItem value="">Todos</MenuItem>
                            <MenuItem value={TipoRubro.Gastronomia}>Gastronomia</MenuItem>
                            <MenuItem value={TipoRubro.Electronica}>Electronica</MenuItem>
                            <MenuItem value={TipoRubro.ArticulosHogar}>Articulos Hogar</MenuItem>
                            <MenuItem value={TipoRubro.Otros}>Otros</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid size={4} key={"puntos"}>
                    <FormControl fullWidth>
                        <TextField
                            fullWidth
                            label={"Puntos maximos necesarios"}
                            value={puntosNecesarios}
                            onChange={(e) => setPuntosNecesarios(e.target.value)}
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
                    premiosIsLoading ?
                        (
                            <CircularProgress/>
                        ) : (
                            (premiosData || [])
                                .map((premio) => (
                                    <PremioCard
                                        id={premio.id}
                                        nombre={premio.nombre}
                                        descripcion={""}
                                        puntos={premio.puntosNecesarios}
                                        imagen={premio.imagen}
                                        rubro={premio.rubro}
                                        key={premio.id}
                                    />
                                ))
                        )
                }
            </Grid>
        </>
    );
}