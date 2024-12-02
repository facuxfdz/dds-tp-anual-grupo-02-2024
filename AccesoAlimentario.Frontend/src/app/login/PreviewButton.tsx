import { Avatar, Typography } from "@mui/material";
import { Box } from "@mui/system";
import Image from 'next/image';


export default function PreviewButton({profile_picture, file_name}: {profile_picture: string | undefined, file_name: string}) {
    return (
        <Box sx={{ 
            display: "flex", 
            flexDirection: "column", 
            alignItems: "center", 
            justifyContent: "center", 
            border: "1px solid #404040", 
            borderRadius: 1, 
            padding: 1,
            backgroundColor: "#484848",            
            // padding
            py: 3,
            px: 1,
            }}>
            {/* Show upload icon in case there is no profile picture */}
            {profile_picture ? (
                <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center" }}>
                <Avatar sx={{ 
                    width: 100, 
                    height: 100,
                }}>
                    <Image src={profile_picture} alt="profile_picture" width={100} height={100} objectFit="cover" />
                </Avatar>
                <Typography variant="body1" color="text.secondary" sx={{ 
                    marginTop: 2,
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    color: "#fff",
                    opacity: 0.8,
                    }}>
                    {file_name}
                </Typography>
                </Box>
                
            ) : (
                <Avatar sx={{ width: 100, height: 100 }} />
            )}
        </Box>
    );
}