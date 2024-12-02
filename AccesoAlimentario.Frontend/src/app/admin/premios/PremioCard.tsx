import MainCard from "@components/Cards/MainCard";
import React from "react";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid2";
import Chip from "@mui/material/Chip";
import {Button, Divider} from "@mui/material";
import {TipoRubro} from "@models/enums/tipoRubro";
import {useAppSelector} from "@redux/hook";
import {usePostCanjeDePremioMutation} from "@redux/services/contribucionesApi";
import {useNotification} from "@components/Notifications/NotificationContext";
import {ICanjeDePremioRequest} from "@models/requests/contribuciones/iCanjeDePremioRequest";

export const PremioCard = ({
                               id,
                               nombre,
                               puntos,
                               imagen,
                               rubro
                           }: {
    id: string,
    nombre: string,
    puntos: number,
    imagen: string,
    rubro: TipoRubro
}) => {
    const user = useAppSelector(state => state.user);
    const [
        postCanjeDePremio,
        {isLoading: canjeDePremioIsLoading}
    ] = usePostCanjeDePremioMutation();
    const {addNotification} = useNotification();

    const handleCanje = async () => {
        const request : ICanjeDePremioRequest = {
            premioId: id,
            colaboradorId: user.id
        };

        try {
            await postCanjeDePremio(request).unwrap();
            addNotification("Premio canjeado con exito", "success");
        } catch {
            addNotification("Error al canjear premio", "error");
        }
    }


    const getRubro = (rubro: TipoRubro) => {
        switch (rubro) {
            case TipoRubro.Gastronomia:
                return "Gastronomia";
            case TipoRubro.Electronica:
                return "Electronica";
            case TipoRubro.ArticulosHogar:
                return "Articulos Hogar";
            case TipoRubro.Otros:
                return "Otros";
        }
    }
    return (
        <Grid size={{
            xs: 12,
            sm: 6,
            md: 4,
            lg: 3
        }} key={id}>
            <MainCard border={false} boxShadow sx={{height: '100%'}}>
                <img src={imagen} alt={nombre} style={{width: '100%', height: '200px', objectFit: 'cover'}}/>

                <Typography variant="h5" component="h2" sx={{padding: '10px 0'}}>
                    {nombre}
                </Typography>

                <Grid container spacing={1} textAlign={"center"} sx={{padding: '10px 0'}}>
                    <Grid size={6}>
                        <Chip label={`${puntos} puntos`} color="secondary"/>
                    </Grid>
                    <Grid size={6}>
                        <Chip label={getRubro(rubro)} color="primary"/>
                    </Grid>
                </Grid>
                <Divider/>
                <Grid container justifyContent="center" sx={{padding: '10px 0'}}>
                    <Grid size={12}>
                        <Button variant="contained" color="primary" fullWidth disabled={canjeDePremioIsLoading} onClick={handleCanje}>
                            Canjear
                        </Button>
                    </Grid>
                </Grid>
            </MainCard>
        </Grid>
    );
}