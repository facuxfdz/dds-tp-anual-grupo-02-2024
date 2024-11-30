import {createSlice, PayloadAction} from '@reduxjs/toolkit';

export interface User {
    id: string;
    name: string;
    email: string;
    profile_picture: string;
}

interface UserTemp {
    name: string;
    email: string;
}

interface UserSlice {
    user: User;
    userTemp: UserTemp;
}

const initialState: UserSlice = {
    user: {
        id: '',
        name: '',
        email: '',
        profile_picture: '',
    },
    userTemp: {
        name: '',
        email: '',
    },
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<User>) => {
            state.user.id = action.payload.id;
            state.user.name = action.payload.name;
            state.user.email = action.payload.email;
            state.user.profile_picture = action.payload.profile_picture
        },
        setSignedUser: (state, action: PayloadAction<UserTemp>) => {
            state.userTemp.name = action.payload.name;
            state.userTemp.email = action.payload.email;
        },
        clearUser: (state) => {
            state.user.id = '';
            state.user.name = '';
            state.user.email = '';
            state.user.profile_picture = '';
        },
    },
});

export const {setUser, clearUser, setSignedUser} = userSlice.actions;
export default userSlice.reducer;
