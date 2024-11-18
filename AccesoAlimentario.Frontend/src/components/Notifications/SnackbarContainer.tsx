import React from "react";
import {Snackbar, Box, Alert} from "@mui/material";
import {Notification} from "./NotificationContext";

interface SnackbarContainerProps {
    notifications: Notification[];
    onRemove: (id: string) => void;
}

const SnackbarContainer: React.FC<SnackbarContainerProps> = ({notifications, onRemove}) => {
    return (
        <Box
            sx={{
                position: "fixed",
                top: 16,
                right: 16,
                zIndex: 9999,
            }}
        >
            {notifications.map((notification) => (
                <SnackbarItem key={notification.id} notification={notification} onRemove={onRemove}/>
            ))}
        </Box>
    );
};

interface SnackbarItemProps {
    notification: Notification;
    onRemove: (id: string) => void;
}

const SnackbarItem: React.FC<SnackbarItemProps> = ({notification, onRemove}) => {
    const {id, message, duration, severity} = notification;

    return (
        <Snackbar
            open
            anchorOrigin={{vertical: "top", horizontal: "right"}}
            onClose={() => onRemove(id)}
            autoHideDuration={duration}
            sx={{
                maxWidth: 300,
                width: "100%"
            }}
        >
            <Alert severity={severity} sx={{width: "100%", textWrap: "wrap"}} onClose={() => onRemove(id)}>
                {message}
            </Alert>
        </Snackbar>
    );
};

export default SnackbarContainer;