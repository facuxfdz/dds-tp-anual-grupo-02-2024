"use client";
import React, {useState} from "react";
import MainCard from "@components/Cards/MainCard";
import {Box, Tab, Tabs} from "@mui/material";
import {CantidadDeFallasPorHeladeraReport} from "@/app/admin/reportes/CantidadDeFallasPorHeladeraReport";
import {
    CantidadDeViandasRetiradasColocadasPorHeladeraReport
} from "@/app/admin/reportes/CantidadDeViandasRetiradasColocadasPorHeladeraReport";
import {
    CantidadDeColaboracionesPorColaboradorReport
} from "@/app/admin/reportes/CantidadDeColaboracionesPorColaboradorReport";

function TabPanel({ children, value, index }: { children: React.ReactNode, value: number, index: number }) {
    return (
        <div role="tabpanel" hidden={value !== index} id={`simple-tabpanel-${index}`} aria-labelledby={`simple-tab-${index}`}>
            {value === index && <Box sx={{ pt: 2 }}>{children}</Box>}
        </div>
    );
}

export default function ReportesPage() {
    const [value, setValue] = useState(0);
    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <MainCard>
            <Box sx={{width: '100%'}}>
                <Box sx={{borderBottom: 1, borderColor: 'divider'}}>
                    <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" centered>
                        <Tab
                            label="Cantidad de Fallas"
                            icon={<i className="fa-duotone fa-solid fa-triangle-exclamation fa-xl"/>} iconPosition="start"
                        />
                        <Tab label="Cantidad de Viandas Retiradas/Colocadas" icon={<i className="fa-duotone fa-solid fa-arrows-rotate fa-xl"/>} iconPosition="start"/>
                        <Tab label="Cantidad de Colaboraciones" icon={<i className="fa-duotone fa-solid fa-cubes fa-xl"/>} iconPosition="start"/>
                    </Tabs>
                </Box>
                <TabPanel value={value} index={0}>
                    <CantidadDeFallasPorHeladeraReport />
                </TabPanel>
                <TabPanel value={value} index={1}>
                    <CantidadDeViandasRetiradasColocadasPorHeladeraReport />
                </TabPanel>
                <TabPanel value={value} index={2}>
                    <CantidadDeColaboracionesPorColaboradorReport />
                </TabPanel>
            </Box>
        </MainCard>
    );
}