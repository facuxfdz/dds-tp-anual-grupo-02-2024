import {createSlice} from "@reduxjs/toolkit";

const initialState = {
    token: "",
    participantId: "1",
    name: "asdasd",
    photo: "/AccesoAlimentario.Icon.png",
};

export const sessionSlice = createSlice({
    name: "session",
    initialState,
    reducers: {
        setToken: (state, action) => {
            state.token = action.payload;
        },
        setParticipantId: (state, action) => {
            state.participantId = action.payload;
        },
    },
});

export const {setToken, setParticipantId} = sessionSlice.actions;

export default sessionSlice.reducer;