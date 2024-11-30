"use client";

import React, { useState } from "react";
import { Box, Stack, Typography, TextField, Button, MenuItem, Select, InputLabel, FormControl, Grid } from "@mui/material";
import MainCard from "@/components/Cards/MainCard";
import { UserData } from "./page";
import { useRegisterQuery } from "@/redux/services/authApi";

interface RegisterPageProps {
  userData: UserData;
}

export default function RegisterPage({ userData }: RegisterPageProps) {
  // Data from Google is in the session jwt token
  const { name, email, user_type, profile_picture, register_type } = userData;

  // State for the form fields
  const [formData, setFormData] = useState({
    name: name,
    email: email,
    password: '',
    user_type: user_type,
    profile_picture: profile_picture,
    file: null as File | null,
  });

  // Handle input changes
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  // Handle file input change
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files ? e.target.files[0] : null;
    setFormData((prev) => ({ ...prev, file: file }));
  };

  // Handle form submission
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    // Handle file upload logic to S3 with presigned URL and form submission logic here
    console.log(formData);
    // useRegisterQuery({
    //   email: formData.email,
    //   password: formData.password,
    //   profile_picture: formData.profile_picture,
    //   register_type: register_type,
    // });

    // Example for handling file upload logic (not implemented here):
    if (formData.file) {
      // Use the backend to get presigned URL and upload the file to S3
      // Example: uploadToS3(formData.file);
    }
  };

  if (!name || !email) {
    return (
      <MainCard sx={{ maxWidth: { xs: 600, lg: 675 }, margin: { xs: 2.5, md: 3 }, '& > *': { flexGrow: 1, flexBasis: '50%' } }} content={false} border={true} boxShadow>
        <Box sx={{ px: 5, py: 5 }}>
          <Typography variant="h2" component="h1" mb={4}>
            Registrarse
          </Typography>
          <Typography variant="body1" component="p" mb={4}>
            No se han podido obtener los datos de Registro. Regresa a la página de inicio e intenta de nuevo.
          </Typography>
          <Button href="/login" variant="contained">
            Regresar
          </Button>
        </Box>
      </MainCard>
    );
  }

  return (
    <MainCard sx={{ maxWidth: { xs: 900, lg: 1275 }, margin: { xs: 2.5, md: 3 }, '& > *': { flexGrow: 1, flexBasis: '50%' } }} content={true} border={true} boxShadow>
      <Box sx={{ px: 10, py: 5 }}>
        <Typography variant="h2" component="h1" mb={4}>
          Registrarse
        </Typography>
        <form onSubmit={handleSubmit}>
          <Stack spacing={3}>
            <TextField
              fullWidth
              label="Nombre"
              name="name"
              value={formData.name}
              onChange={handleChange}
              required
            />
            <TextField
              fullWidth
              label="Correo electrónico"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
              type="email"
            />
            {register_type === "standard" && (
            <TextField
              fullWidth
              label="Contraseña"
              name="password"
              type="password"
              value={formData.password}
              onChange={handleChange}
              required
            />
            )}
            <FormControl fullWidth required>
              <InputLabel>Tipo de usuario</InputLabel>
              <Select
                name="user_type"
                value={formData.user_type}
                onChange={(e) => setFormData((prev) => ({ ...prev, user_type: e.target.value as "persona_fisica" | "persona_juridica" | "tecnico" }))}
                label="Tipo de usuario"
              >
                <MenuItem value="persona_fisica">Persona Física</MenuItem>
                <MenuItem value="persona_juridica">Persona Jurídica</MenuItem>
                <MenuItem value="tecnico">Técnico</MenuItem>
              </Select>
            </FormControl>
            <Button
              fullWidth
              variant="outlined"
              component="label"
              color="primary"
            >
              Subir Foto de Perfil
              <input
                type="file"
                hidden
                onChange={handleFileChange}
              />
            </Button>
            {formData.file && (
              <Typography variant="body2" color="textSecondary" sx={{ mt: 2 }}>
                {formData.file.name}
              </Typography>
            )}
            <Button type="submit" variant="contained" fullWidth>
              Registrarse
            </Button>
          </Stack>
        </form>
      </Box>
    </MainCard>
  );
}
