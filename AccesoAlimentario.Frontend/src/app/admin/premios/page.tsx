"use client";
import {PremioCard} from "@/app/admin/premios/PremioCard";
import {Button, CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import Grid from "@mui/material/Grid2";
import React, {useEffect, useState} from "react";
import {useLazyGetPremiosQuery} from "@redux/services/contribucionesApi";
import {TipoRubro} from "@models/enums/tipoRubro";
import {useLazyGetPremiosReclamadosQuery} from "@redux/services/colaboradoresApi";
import {useAppSelector} from "@redux/hook";

export default function PremiosPage() {
    const [rubro, setRubro] = useState<TipoRubro | "todos">("todos");
    const [puntosNecesarios, setPuntosNecesarios] = useState<number | "">("");
    const user = useAppSelector(state => state.user);
    const [nombre, setNombre] = useState('');
    const [verReclamador, setVerReclamador] = useState(false);
    const [
        getPremios,
        {data: premiosData, isLoading: premiosIsLoading}
    ] = useLazyGetPremiosQuery();
    const [
        getPremiosReclamados,
        {data: premiosReclamadosData, isLoading: premiosReclamadosIsLoading}
    ] = useLazyGetPremiosReclamadosQuery();

    useEffect(() => {
        if (!verReclamador) {
            getPremios({
                nombre,
                puntosNecesarios: puntosNecesarios === "" ? undefined : puntosNecesarios,
                rubro: typeof rubro === 'string' ? undefined : rubro
            });
        }
    }, [getPremios, nombre, puntosNecesarios, rubro, verReclamador]);

    useEffect(() => {
        if (verReclamador) {
            getPremiosReclamados({
                colaboradorId: user.colaboradorId,
                nombre,
                puntosNecesarios: puntosNecesarios === "" ? undefined : puntosNecesarios,
                rubro: typeof rubro === 'string' ? undefined : rubro
            });
        }
    }, [getPremiosReclamados, nombre, puntosNecesarios, rubro, user.colaboradorId, verReclamador]);

    return (
        <>
            <Grid container spacing={3} mb={3} justifyContent={"center"} alignItems={"center"}>
                <Grid size={3} key={"categoria"}>
                    <FormControl fullWidth>
                        <InputLabel id="categoria-label">Categoria</InputLabel>
                        <Select
                            labelId="categoria-label"
                            value={rubro}
                            onChange={(e) => setRubro(e.target.value as TipoRubro)}
                            label="Categoria"
                        >
                            <MenuItem value={"todos"}>Todos</MenuItem>
                            <MenuItem value={TipoRubro.Gastronomia}>Gastronomia</MenuItem>
                            <MenuItem value={TipoRubro.Electronica}>Electronica</MenuItem>
                            <MenuItem value={TipoRubro.ArticulosHogar}>Articulos Hogar</MenuItem>
                            <MenuItem value={TipoRubro.Otros}>Otros</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid size={3} key={"puntos"}>
                    <FormControl fullWidth>
                        <TextField
                            fullWidth
                            label={"Puntos maximos necesarios"}
                            value={puntosNecesarios}
                            onChange={(e) => setPuntosNecesarios(e.target.value === "" ? "" : parseInt(e.target.value))}
                            variant="outlined"
                            type={"number"}
                        />
                    </FormControl>
                </Grid>
                <Grid size={3} key={"nombre"}>
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
                <Grid size={3} key={"reclamados"}>
                    <Button
                        onClick={() => setVerReclamador(!verReclamador)}
                        variant={"contained"}
                        fullWidth
                    >
                        {verReclamador ? "Ver todos" : "Ver reclamados"}
                    </Button>
                </Grid>
            </Grid>
            <Grid container spacing={3}>
                {
                    premiosIsLoading || premiosReclamadosIsLoading ?
                        (
                            <CircularProgress/>
                        ) : (
                            ((verReclamador ? premiosReclamadosData : premiosData) || [])
                                .map((premio) => (
                                    <PremioCard
                                        id={premio.id}
                                        nombre={premio.nombre}
                                        puntos={premio.puntosNecesarios}
                                        imagen={premio.imagen}
                                        rubro={premio.rubro}
                                        key={premio.id}
                                        reclamable={!verReclamador}
                                    />
                                ))
                        )
                }
            </Grid>
        </>
    );
}