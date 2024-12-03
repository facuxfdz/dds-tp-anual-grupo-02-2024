import React from "react";
import MainLayout from "@components/Layouts/MainLayout"

export default function Layout({
                                   children,
                               }: {
    children: React.ReactNode;
}) {
    return (
        <MainLayout>
            {children}
        </MainLayout>
    );
}