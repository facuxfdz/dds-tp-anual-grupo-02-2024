'use client';

import React from "react";
import AuthWrapper from "@/components/Auth/AuthWrapper";
import Register from "./Register";
import { Box } from "@mui/system";
import { useAppSelector } from "@/redux/hook";
import { parseJwt } from "@/utils/decode_jwt";

export interface UserData {
  name: string;
  email: string;
  user_type: "Humana" | "Juridica";
  profile_picture?: string;
  register_type: "sso" | "standard";
  sexo?: string;
  rubro?: string;
  tipoJuridico?: string;
  direccion?: any;
};



export default function Page() {
  // Retrieve user google data from the session token
  const user = useAppSelector(state => state.user.userTemp); 

  const userData: UserData = {
    name: user.name,
    email: user.email,
    user_type: "Humana",
    profile_picture: user.profile_picture,
    register_type: user.register_type
  };

  return (
    <Box 
        sx={{ 
            minHeight: "100vh",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",            
            py: 3,
        }}
    >
    <Register userData={userData} />
    </Box>
  );
}
