import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

interface User {
    // Si es colaborador, el id es el del colaborador
    // Si es tecnico, el id es el del tecnico
    id: string;

    name: string;
    profile_picture: string;

    constribucionesPreferidas: ContribucionesTipo[];
    personaTipo: 'humana' | 'juridica';
    role: 'colaborador' | 'tecnico' | '';
}

const initialState: User = {
    id: '1a5f2333-f5e4-4b77-a727-ebf880de41de',

    name: 'Marcos Pedaci',
    profile_picture: '',

    constribucionesPreferidas: [],
    personaTipo: 'humana',
    role: 'colaborador',
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<User>) => {
            state.id = action.payload.id;
            state.name = action.payload.name;

            state.profile_picture = action.payload.profile_picture;

            state.constribucionesPreferidas = action.payload.constribucionesPreferidas;
            state.personaTipo = action.payload.personaTipo;
            state.role = action.payload.role;
        },
        clearUser: (state) => {
            state.id = '';
            state.name = '';

            state.profile_picture = '';

            state.constribucionesPreferidas = [];
            state.personaTipo = 'humana';
            state.role = '';
        },
    },
});

export const {setUser, clearUser} = userSlice.actions;
export default userSlice.reducer;
