"use client";

import React, { useState } from "react";
import { Box, Stack } from "@mui/material";
import Grid from "@mui/material/Grid2";
import Typography from "@mui/material/Typography";
import { TextField, Button } from "@mui/material";
import MainCard from "@/components/Cards/MainCard";
import { GoogleUserData } from "./page";

interface RegisterPageProps {
    googleUserData: GoogleUserData;
}

export default function RegisterPage({googleUserData} : RegisterPageProps) {
  // Data from Google is in the session jwt token
  const { name, email, address } = googleUserData;
  // State for the form fields
  const [formData, setFormData] = useState({
    name: name,
    email: email,
    address: address,
  });

  // Handle input changes
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  // Handle form submission
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Submitted data:", formData);
    // Add API call logic here
  };

  if(!name || !email) {
    return (
      <MainCard sx={{ maxWidth: { xs: 600, lg: 675 }, margin: { xs: 2.5, md: 3 }, '& > *': { flexGrow: 1, flexBasis: '50%' } }} content={false} border={true} boxShadow>
        <Box
        sx={{        
          px: 5,
          py: 5
        }}
      >
          <Typography variant="h2" component="h1" mb={4}>
              Registrarse
          </Typography>
          <Typography variant="body1" component="p" mb={4}>
              No se han podido obtener los datos de Google. Por favor, intenta registrarte de nuevo.
          </Typography>
      </Box>
      </MainCard>
    );
  }

  return (
    <MainCard sx={{ maxWidth: { xs: 600, lg: 675 }, margin: { xs: 2.5, md: 3 }, '& > *': { flexGrow: 1, flexBasis: '50%' } }} content={false} border={true} boxShadow>
      <Box
      sx={{        
        px: 5,
        py: 5
      }}
    >
        <Typography variant="h2" component="h1" mb={4}>
            Registrarse
        </Typography>
        <form onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <TextField
                    fullWidth
                    label="Nombre"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                />
                <TextField
                    fullWidth
                    label="Correo electrónico"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                />
                <TextField
                    fullWidth
                    label="Dirección"
                    name="address"
                    value={formData.address}
                    onChange={handleChange}
                />
                <Button type="submit" variant="contained">
                    Registrarse
                </Button>
            </Stack>
            </form>
    </Box>
    </MainCard>
  );
}
