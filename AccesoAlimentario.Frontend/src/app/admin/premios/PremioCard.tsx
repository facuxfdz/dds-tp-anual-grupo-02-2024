import MainCard from "@components/Cards/MainCard";
import React from "react";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid2";
import Chip from "@mui/material/Chip";
import {Button, Divider} from "@mui/material";
import Image from 'next/image';

export const PremioCard = ({
                               id,
                               nombre,
                               descripcion,
                               puntos,
                               imagen,
                               categoria
                           }: {
    id: number,
    nombre: string,
    descripcion: string,
    puntos: number,
    imagen: string,
    categoria: string
}) => {

    return (
        <Grid size={3} key={id}>
            <MainCard border={false} boxShadow sx={{height: '100%'}}>
                <Image src={imagen} alt={nombre} width={300} height={200} objectFit="cover"/>

                <Typography variant="h5" component="h2" sx={{padding: '10px 0'}}>
                    {nombre}
                </Typography>

                <Typography variant="body2" color="text.secondary">
                    {descripcion}
                </Typography>

                <Grid container spacing={1} textAlign={"center"} sx={{padding: '10px 0'}}>
                    <Grid size={6}>
                        <Chip label={`${puntos} puntos`} color="secondary"/>
                    </Grid>
                    <Grid size={6}>
                        <Chip label={categoria} color="primary"/>
                    </Grid>
                </Grid>
                <Divider/>
                <Grid container justifyContent="center" sx={{padding: '10px 0'}}>
                    <Grid size={12}>
                        <Button variant="contained" color="primary" fullWidth>
                            Canjear
                        </Button>
                    </Grid>
                </Grid>
            </MainCard>
        </Grid>
    );
}