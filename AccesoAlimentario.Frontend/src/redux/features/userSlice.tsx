import {createSlice, PayloadAction} from '@reduxjs/toolkit';

interface User {
    id: string;
    name: string;
    email: string;
    profile_picture: string;
}

const initialState: User = {
    id: '',
    name: '',
    email: '',
    profile_picture: '',
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<User>) => {
            state.id = action.payload.id;
            state.name = action.payload.name;
            state.email = action.payload.email;
            state.profile_picture = action.payload.profile_picture
        },
        clearUser: (state) => {
            state.id = '';
            state.name = '';
            state.email = '';
            state.profile_picture = '';
        },
    },
});

export const {setUser, clearUser} = userSlice.actions;
export default userSlice.reducer;
