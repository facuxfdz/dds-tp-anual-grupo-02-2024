'use client';

import React from "react";
import AuthWrapper from "@/components/Auth/AuthWrapper";
import Register from "./Register";
import { Box } from "@mui/system";
import { useAppSelector } from "@/redux/hook";
import { parseJwt } from "@/utils/decode_jwt";

export interface GoogleUserData {
  name: string;
  email: string;
  address: string;
};

export default function Page() {
  // Retrieve user google data from the session token
  const user = useAppSelector(state => state.user.userTemp);  
  const googleUserData: GoogleUserData = {
    name: user.name!,
    email: user.email!,
    address: ""
  };
  return (
    <Box 
        sx={{ 
            minHeight: "100vh",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
            // Padding vertical 3
            py: 3,
        }}
    >
    <Register googleUserData={googleUserData} />
    </Box>
  );
}
