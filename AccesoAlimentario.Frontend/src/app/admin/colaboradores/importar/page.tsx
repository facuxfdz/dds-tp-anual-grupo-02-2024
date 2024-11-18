"use client";
import {Box, Button, CardActions, Divider, Icon, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import MainCard from "@components/Cards/MainCard";
import React from "react";
import {styled, useTheme} from "@mui/material/styles";
import {useDropzone} from "react-dropzone";
import Grid from "@mui/material/Grid2";
import {usePostImportarColaboradoresCsvMutation} from "@redux/services/colaboradoresApi";
import {useNotification} from "@components/Notifications/NotificationContext";

const DropzoneWrapper = styled('div')(({theme}) => ({
    outline: 'none',
    overflow: 'hidden',
    position: 'relative',
    padding: theme.spacing(5, 1),
    borderRadius: theme.shape.borderRadius,
    transition: theme.transitions.create('padding'),
    backgroundColor: theme.palette.background.paper,
    border: `1px dashed ${theme.palette.secondary.main}`,
    '&:hover': {opacity: 0.72, cursor: 'pointer'}
}));

export default function ColaboradoresImportarPage() {
    const theme = useTheme();
    const [file, setFile] = React.useState<string>("");
    const [fileName, setFileName] = React.useState<string>("");
    const {getRootProps, getInputProps, isDragActive, isDragReject} = useDropzone({
        accept: {
            'text/csv': []
        },
        multiple: false,
        onDrop: (acceptedFiles) => {
            const file = acceptedFiles[0];
            const reader = new FileReader();
            reader.onload = () => {
                setFile(reader.result as string);
                setFileName(file.name);
            };
            reader.readAsDataURL(file);
        }
    });
    const [postImportarColaboradoresCsv, {
        isLoading: isImporting,
        isError,
    }] = usePostImportarColaboradoresCsvMutation();
    const {addNotification} = useNotification();

    const handleSave = async () => {
        if (file) {
            await postImportarColaboradoresCsv({
                archivo: file.split(",")[1]
            }).unwrap()
            if (isError) {
                addNotification("Ocurrió un error al importar los colaboradores", "error");
            } else {
                addNotification("Colaboradores importados correctamente", "success");
            }
        } else {
            addNotification("Debe seleccionar un archivo", "error");
        }
    }

    return (
        <MainCard content={false} sx={{overflow: 'visible'}}>
            <CardActions
                sx={{
                    position: 'sticky',
                    top: '60px',
                    bgcolor: theme.palette.background.default,
                    zIndex: 1,
                    borderBottom: `1px solid ${theme.palette.divider}`
                }}
            >
                <Stack direction="row" alignItems="center" justifyContent="space-between"
                       sx={{width: 1}}>
                    <Box component="div" sx={{flexGrow: 1, m: 0, pl: 1.5}}>
                        <Typography variant="h5" sx={{flexGrow: 1}}>
                            Importar colaboradores
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Importar colaboradores desde un archivo CSV
                        </Typography>
                    </Box>
                </Stack>
            </CardActions>
            <CardContent>
                <Grid container spacing={3}>
                    <Grid size={12}>
                        {
                            file ? (
                                <Stack
                                    spacing={2}
                                    alignItems="center"
                                    justifyContent="center"
                                    direction={'column'}
                                    sx={{width: 1}}
                                >
                                    <Stack
                                        spacing={2}
                                        alignItems="center"
                                        justifyContent="center"
                                        direction={{xs: 'column', md: 'row'}}
                                        sx={{width: 1, textAlign: {xs: 'center', md: 'left'}}}
                                    >
                                        <Icon sx={{width: "auto"}} className="fa-duotone fa-solid fa-file-csv"
                                              fontSize={"large"}/>
                                        <Stack sx={{p: 2}} spacing={1}>
                                            <Typography variant="h5">Archivo seleccionado</Typography>
                                            <Typography color="secondary">
                                                {fileName}
                                            </Typography>
                                        </Stack>
                                    </Stack>
                                    <Button
                                        variant="contained"
                                        size="small"
                                        onClick={() => {
                                            setFile("");
                                            setFileName("");
                                        }}
                                        endIcon={<i className="fa-sharp-duotone fa-solid fa-trash"></i>}
                                        color="error"
                                        disabled={isImporting}
                                        sx={{
                                            mt: 2,
                                        }}
                                    >
                                        Cambiar archivo
                                    </Button>
                                </Stack>
                            ) : (
                                <DropzoneWrapper
                                    {...getRootProps()}
                                    sx={{
                                        ...(isDragActive && {opacity: 0.72}),
                                        ...((isDragReject) && {
                                            color: 'error.main',
                                            borderColor: 'error.light',
                                            bgcolor: 'error.lighter'
                                        }),
                                    }}
                                >
                                    <input {...getInputProps()} />
                                    <Stack
                                        spacing={2}
                                        alignItems="center"
                                        justifyContent="center"
                                        direction={{xs: 'column', md: 'row'}}
                                        sx={{width: 1, textAlign: {xs: 'center', md: 'left'}}}
                                    >
                                        <Icon sx={{width: "auto"}} className="fa-duotone fa-solid fa-cloud-arrow-up"
                                              fontSize={"large"}/>
                                        <Stack sx={{p: 3}} spacing={1}>
                                            <Typography variant="h5">Arrastrar y soltar o seleccionar
                                                archivo</Typography>
                                            <Typography color="secondary">
                                                Suelte el archivo aquí o haga clic&nbsp;
                                                <Typography component="span" color="primary"
                                                            sx={{textDecoration: 'underline'}}>
                                                    navegar
                                                </Typography>
                                                &nbsp;en su computadora
                                            </Typography>
                                        </Stack>
                                    </Stack>
                                </DropzoneWrapper>
                            )
                        }
                    </Grid>
                </Grid>
            </CardContent>
            <Divider/>
            <CardActions sx={{
                bgcolor: theme.palette.background.default,
            }}>
                <Stack direction="row" spacing={1} justifyContent="center"
                       sx={{width: 1, px: 1.5, py: 0.75}}>
                    <Button color="primary" variant="contained" type={"submit"} disabled={!file || isImporting}
                            onClick={handleSave}>
                        Importar
                    </Button>
                </Stack>
            </CardActions>
        </MainCard>
    );
}