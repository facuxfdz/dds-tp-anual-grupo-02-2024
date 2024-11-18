"use client";
import React, { createContext, useContext, useState, ReactNode } from "react";
import { v4 as uuidv4 } from "uuid";
import SnackbarContainer from "@components/Notifications/SnackbarContainer"; // Importa la función v4 de uuid

type Severity = "success" | "error" | "warning" | "info";

export interface Notification {
    id: string;
    message: string;
    duration: number;
    severity: Severity;
}

interface NotificationContextType {
    addNotification: (message: string, severity: Severity, duration?: number) => void;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotification = () => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error("useNotification debe ser usado dentro de un NotificationProvider");
    }
    return context;
};

export const NotificationProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [notifications, setNotifications] = useState<Notification[]>([]);

    const addNotification = (message: string, severity: Severity, duration = 3000) => {
        const id = uuidv4();
        setNotifications((prev) => [...prev, { id, message, duration, severity }]);
    };

    const removeNotification = (id: string) => {
        setNotifications((prev) => prev.filter((notification) => notification.id !== id));
    };

    return (
        <NotificationContext.Provider value={{ addNotification }}>
            {children}
            <SnackbarContainer notifications={notifications} onRemove={removeNotification} />
        </NotificationContext.Provider>
    );
};
