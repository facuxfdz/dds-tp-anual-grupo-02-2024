import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    logo: "/AccesoAlimentario.Logo.png",
    icon: "/AccesoAlimentario.Icon.png",
    primaryColor: "#1D1C1A",
    secondaryColor: "#1D1C1A",
};

export const customizationSlice = createSlice({
    name: "customization",
    initialState: initialState,
    reducers: {
        setCustomization: (state, action) => {
            state.primaryColor = action.payload.primaryColor;
            state.secondaryColor = action.payload.secondaryColor;
            state.logo = action.payload.logo;
        },
        resetCustomization: (state) => {
            state.primaryColor = initialState.primaryColor;
            state.secondaryColor = initialState.secondaryColor;
            state.logo = initialState.logo;
        },
    },
});

export const { setCustomization, resetCustomization } = customizationSlice.actions;

export default customizationSlice.reducer;