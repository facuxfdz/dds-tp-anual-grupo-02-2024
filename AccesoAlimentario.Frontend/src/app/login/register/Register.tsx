"use client";

import React, { useEffect, useState } from "react";
import { Box, Stack, Typography, TextField, Button, MenuItem, Select, InputLabel, FormControl, Grid } from "@mui/material";
import MainCard from "@/components/Cards/MainCard";
import { UserData } from "./page";
import { useLazyRegisterQuery, useRegisterQuery } from "@/redux/services/authApi";
import PreviewButton from "../PreviewButton";
import { useRouter } from "next/navigation";
import { setUser } from "@/redux/features/userSlice";
import { useDispatch } from "react-redux";

interface RegisterPageProps {
  userData: UserData;
}

export default function RegisterPage({ userData }: RegisterPageProps) {
  // Data from Google is in the session jwt token
  const { 
    name, 
    email, 
    user_type, 
    profile_picture, 
    register_type,
    sexo,
    rubro,
    tipoJuridico
   } = userData;
  const router = useRouter();
  const dispatch = useDispatch();
  // use lazy query
  const [register, { data, error }] = useLazyRegisterQuery();

  // State for the form fields
  const [formData, setFormData] = useState({
    name: name,
    email: email,
    password: '',
    user_type: user_type,
    profile_picture: profile_picture,
    file: null as File | null,
    sexo: sexo,
    rubro: rubro,
    tipoJuridico: tipoJuridico,

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
    setFormData((prev) => ({ ...prev, profile_picture: file ? URL.createObjectURL(file) : '' }));
  };



  // Handle form submission
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    // Handle file upload logic to S3 with presigned URL and form submission logic here
    console.log(formData);
    register({
      email: formData.email,
      password: formData.password,
      profile_picture: formData.profile_picture,
      register_type: register_type,
      user_type: formData.user_type,
      direccion: {
        calle: "calle",
        numero: "numero",
        localidad: "localidad",
        codigoPostal: "codigoPostal",
      },
      documento: {
        tipoDocumento: "DNI",
        nroDocumento: 12345678,
        fechaNacimiento: "2000-01-01",
      },
      persona: {
        nombre: `${formData.name.split(' ')[0]}`,
        apellido: `${formData.name.split(' ')[1]} ${formData.name.split(' ').length > 2 ? formData.name.split(' ')[2] : ''}`,
        tipo: formData.user_type,
        sexo: formData.sexo,
        rubro: formData.rubro,
        tipoJuridico: formData.tipoJuridico,
      }
    });    
    // Example for handling file upload logic (not implemented here):
    if (formData.file) {
      // Use the backend to get presigned URL and upload the file to S3
      // Example: uploadToS3(formData.file);
    }    
  };

  useEffect(() => {
    if (data) {
      dispatch(setUser({
        name: formData.name,
        email: formData.email,
        profile_picture: formData.profile_picture
      }));
      router.replace('/admin/inicio');
      
    }
  }, [data]);


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
                onChange={(e) => setFormData((prev) => ({ ...prev, user_type: e.target.value as "Humana" | "Juridica" }))}
                label="Tipo de usuario"
              >
                <MenuItem value="Humana">Persona Física</MenuItem>
                <MenuItem value="Juridica">Persona Jurídica</MenuItem>
              </Select>
            </FormControl>
            {/* Display button first  */}
            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              <PreviewButton profile_picture={formData.profile_picture} file_name= {formData.file ? formData.file.name : 'sso_profile_picture'} />
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
            </Box>
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
