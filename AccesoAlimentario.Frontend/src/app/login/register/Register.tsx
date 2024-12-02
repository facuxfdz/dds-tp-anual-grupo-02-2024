"use client";

import React, { useEffect, useState } from "react";
import {
  Box,
  Stack,
  Typography,
  TextField,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  SelectChangeEvent,
} from "@mui/material";
import MainCard from "@/components/Cards/MainCard";
import { UserData } from "./page";
import { useLazyRegisterQuery } from "@/redux/services/authApi";
import PreviewButton from "../PreviewButton";
import { useRouter } from "next/navigation";
import { setUser } from "@/redux/features/userSlice";
import { useDispatch } from "react-redux";
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

interface RegisterPageProps {
  userData: UserData;
}

export default function RegisterPage({ userData }: RegisterPageProps) {
  const {
    name,
    email,
    user_type,
    profile_picture,
    register_type,
    sexo,
    rubro,
    tipoJuridico,
  } = userData;

  const router = useRouter();
  const dispatch = useDispatch();
  const [register, { data }] = useLazyRegisterQuery();

  const [formData, setFormData] = useState({
    name,
    email,
    password: "",
    user_type,
    profile_picture,
    file: null as File | null,
    sexo,
    rubro: "",
    tipoJuridico: "" as "Gubernamental" | "Ong" | "Empresa" | "Institucion",
    direccion: {
      calle: "",
      numero: "",
      localidad: "",
      codigoPostal: "",
    },
    documento: {
      tipoDocumento: "" as "DNI" | "LE" | "LC" | "CUIT" | "CUIL",
      nroDocumento: "",
      fechaNacimiento: new Date().toISOString(),
    },
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUserTypeChange = (e: SelectChangeEvent<"Humana" | "Juridica">) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  }

  const handleTipoDocumentoChange = (e: SelectChangeEvent<"DNI" | "LE" | "LC" | "CUIT" | "CUIL">) => {
    const value = e.target.value as "DNI" | "LE" | "LC" | "CUIT" | "CUIL";    
    setFormData((prev) => ({ ...prev, documento: { ...prev.documento, tipoDocumento: value } }));
  }

  const handleTipoJuridicoChange = (e: SelectChangeEvent<"Gubernamental" | "Ong" | "Empresa" | "Institucion">) => {
    const value = e.target.value as "Gubernamental" | "Ong" | "Empresa" | "Institucion";
    setFormData((prev) => ({ ...prev, tipoJuridico: value }));
  }

  const handleNumeroDocumentoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFormData((prev) => ({ ...prev, documento: { ...prev.documento, nroDocumento: value } }));
  }

  const handleDirectionCalleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFormData((prev) => ({ ...prev, direccion: { ...prev.direccion, calle: value } }));
  }

  const handleDireccionNumeroChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFormData((prev) => ({ ...prev, direccion: { ...prev.direccion, numero: value } }));
  }

  const handleDireccionLocalidadChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFormData((prev) => ({ ...prev, direccion: { ...prev.direccion, localidad: value } }));
  }

  const handleDireccionCodigoPostalChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFormData((prev) => ({ ...prev, direccion: { ...prev.direccion, codigoPostal: value } }));
  }

  const handleDocumentoFechaNacimientoChange = (val: string | null) => {
    console.log(val)
    const date = Date.parse(val || "") || Date.now();
    const dateStr = new Date(date).toISOString();
    setFormData((prev) => ({ ...prev, documento: { ...prev.documento, fechaNacimiento: dateStr } }));
  }

  const handleSexoChange = (e: SelectChangeEvent<"Masculino" | "Femenino" | "Otro">) => {
    const value = e.target.value as "Masculino" | "Femenino" | "Otro";
    setFormData((prev) => ({ ...prev, sexo: value }));
  }

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files ? e.target.files[0] : null;
    setFormData((prev) => ({
      ...prev,
      file,
      profile_picture: file ? URL.createObjectURL(file) : "",
    }));
  };

  const validSubmit = (data: typeof formData) => {
    const { name, email, password, user_type, documento, direccion } = data;
    // Check that nroDocumento can be parsed as a number
    if (documento.nroDocumento && isNaN(Number(documento.nroDocumento))) {
      return false;
    }
    return name && email && user_type && direccion.calle && direccion.numero && direccion.localidad && direccion.codigoPostal;
  }
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validSubmit(formData)) {
      alert("Por favor complete todos los campos");
      return;
    }
    console.log(formData);
    register({
      email: formData.email,
      password: formData.password,
      profile_picture: formData.profile_picture,
      register_type: register_type,
      user_type: formData.user_type,
      direccion: {
        calle: formData.direccion.calle,
        numero: formData.direccion.numero,
        localidad: formData.direccion.localidad,
        codigoPostal: formData.direccion.codigoPostal,
      },
      documento: {
        tipoDocumento: formData.user_type === "Juridica" ? "CUIT" : formData.documento.tipoDocumento, // Explicitly handle type
        nroDocumento: parseInt(formData.documento.nroDocumento),
        fechaNacimiento: formData.documento.fechaNacimiento,
      },
      persona: {
        nombre: `${formData.name.split(' ')[0]}`,
        apellido: `${formData.name.split(' ')[1]} ${formData.name.split(' ').length > 2 ? formData.name.split(' ')[2] : ''}`,
        tipo: formData.user_type,
        sexo: formData.sexo,
        rubro: formData.rubro,
        tipoJuridico: formData.tipoJuridico,
      },
    });
    
  };

  useEffect(() => {
    if (data) {
      dispatch(
        setUser({
          name: formData.name,
          email: formData.email,
          profile_picture: formData.profile_picture,
        })
      );
      router.replace("/admin/inicio");
    }
  }, [data]);

  const isJuridica = formData.user_type === "Juridica";

  return (
    <MainCard
      sx={{
        maxWidth: { xs: 900, lg: 1275 },
        margin: { xs: 2.5, md: 3 },
        "& > *": { flexGrow: 1, flexBasis: "50%" },
      }}
      content={true}
      border={true}
      boxShadow
    >
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
                onChange={handleUserTypeChange}
                label="Tipo de usuario"
              >
                <MenuItem value="Humana">Persona Física</MenuItem>
                <MenuItem value="Juridica">Persona Jurídica</MenuItem>
              </Select>
            </FormControl>
            <FormControl fullWidth required>
              <InputLabel>Tipo de documento</InputLabel>
              <Select
                name="documento.tipoDocumento"
                value={formData.documento.tipoDocumento}
                onChange={handleTipoDocumentoChange}
                label="Tipo de documento"
              >
                {isJuridica ? (
                  <MenuItem value="CUIT">CUIT</MenuItem>
                ) : (
                    ["DNI", "LE", "LC", "CUIL"].map((tipo) => (
                      <MenuItem key={tipo} value={tipo}>
                        {tipo}
                      </MenuItem>
                    ))
                )}
              </Select>
            </FormControl>
            <TextField
              fullWidth
              label="Número de documento"
              name="documento.nroDocumento"
              value={formData.documento.nroDocumento}
              onChange={handleNumeroDocumentoChange}
              required
            />
            {!isJuridica && (
              <LocalizationProvider dateAdapter={AdapterDateFns}>
              <DatePicker
                label="Fecha de nacimiento"
                name="documento.fechaNacimiento"
                value={formData.documento.fechaNacimiento.toString()}
                onChange={handleDocumentoFechaNacimientoChange}
              />
              <FormControl fullWidth>
                <InputLabel>Sexo</InputLabel>
                <Select
                  name="sexo"
                  value={formData.sexo as "Masculino" | "Femenino" | "Otro"}
                  onChange={handleSexoChange}
                  label="Sexo"
                >
                  <MenuItem value="Masculino">Masculino</MenuItem>
                  <MenuItem value="Femenino">Femenino</MenuItem>
                  <MenuItem value="Otro">Xd</MenuItem>
                </Select>
              </FormControl>
              </LocalizationProvider>
            )}
            {isJuridica && (
              <>
                <TextField
                  fullWidth
                  label="Rubro"
                  name="rubro"
                  value={formData.rubro}
                  onChange={handleChange}
                />
                <FormControl fullWidth>
                  <InputLabel>Tipo Jurídico</InputLabel>
                  <Select
                    name="tipoJuridico"
                    value={formData.tipoJuridico}
                    onChange={handleTipoJuridicoChange}
                    label="Tipo Jurídico"
                  >
                    <MenuItem value="Gubernamental">Gubernamental</MenuItem>
                    <MenuItem value="Ong">Ong</MenuItem>
                    <MenuItem value="Empresa">Empresa</MenuItem>
                    <MenuItem value="Institucion">Institución</MenuItem>
                  </Select>
                </FormControl>
              </>
            )}
            <TextField
              fullWidth
              label="Calle"
              name="direccion.calle"
              value={formData.direccion.calle}
              onChange={handleDirectionCalleChange}
              required
            />
            <TextField
              fullWidth
              label="Número"
              name="direccion.numero"
              value={formData.direccion.numero}
              onChange={handleDireccionNumeroChange}
              required
            />
            <TextField
              fullWidth
              label="Localidad"
              name="direccion.localidad"
              value={formData.direccion.localidad}
              onChange={handleDireccionLocalidadChange}
              required
            />
            <TextField
              fullWidth
              label="Código Postal"
              name="direccion.codigoPostal"
              value={formData.direccion.codigoPostal}
              onChange={handleDireccionCodigoPostalChange}
              required
            />
            <Box>
              <PreviewButton
                profile_picture={formData.profile_picture}
                file_name={
                  formData.file ? formData.file.name : "sso_profile_picture"
                }
              />
              <Button variant="outlined" component="label" color="primary">
                Subir Foto de Perfil
                <input type="file" hidden onChange={handleFileChange} />
              </Button>
            </Box>
            <Button type="submit" variant="contained" fullWidth>
              Registrarse
            </Button>
          </Stack>
        </form>
      </Box>
    </MainCard>
  );
}
