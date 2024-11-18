import { createSlice } from "@reduxjs/toolkit";
import { PaletteMode } from '@mui/material';

const initialState = {
    mode: "light" as PaletteMode,
    drawerOpen: false,
    fontFamily: "Public Sans",
};

export const themeSlice = createSlice({
    name: "theme",
    initialState,
    reducers: {
        changeMode: (state) => {
            state.mode = state.mode === "light" ? "dark" : "light";
        },
        changeDrawer: (state) => {
            state.drawerOpen = !state.drawerOpen;
        },
    },
});

export const { changeMode, changeDrawer } = themeSlice.actions;

export default themeSlice.reducer;