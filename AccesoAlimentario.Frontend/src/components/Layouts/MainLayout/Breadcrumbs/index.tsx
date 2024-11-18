"use client";
import {usePathname} from "next/navigation";
import {Breadcrumbs as MuiBreadcrumbs, Grid2 as Grid} from "@mui/material";
import Typography from "@mui/material/Typography";
import Link from '@mui/material/Link';
import NextLink from 'next/link';
import {useEffect, useState} from "react";
import {GetBreadcrumbs, IBreadcrumbItem} from "@utils/getBreadcrumbs";
import MainCard from "@components/Cards/MainCard";

const Breadcrumbs = () => {
    const pathName = usePathname();
    const [breads, setBreads] = useState<IBreadcrumbItem[]>([]);

    useEffect(() => {
        const b = GetBreadcrumbs(pathName);
        setBreads(b);
    }, [pathName]);

    return (
        <MainCard shadow={"none"} sx={{mb: 3, bgcolor: 'transparent'}} border={false} content={false}>
            <Grid
                container
                direction="row"
                justifyContent="space-between"
                alignItems="center"
                spacing={1}
            >
                <MuiBreadcrumbs separator="›" aria-label="breadcrumb">
                    {breads.map((bread, index) => {
                        if (bread.url === "#" || index === breads.length - 1) {
                            return (
                                <Typography key={index} sx={{color: 'text.primary'}}>
                                    {bread.title}
                                </Typography>
                            )
                        } else {
                            return (
                                <Link component={NextLink} underline="hover" key={index} color="inherit"
                                      href={bread.url}>
                                    {bread.title}
                                </Link>
                            )
                        }
                    })}
                </MuiBreadcrumbs>
                <Grid size={12} sx={{mt: 0.25}}>
                    <Typography variant="h2">{breads[breads.length - 1]?.title}</Typography>
                </Grid>
            </Grid>
        </MainCard>
    )
}

export default Breadcrumbs;