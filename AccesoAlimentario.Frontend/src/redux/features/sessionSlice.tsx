import {createSlice} from "@reduxjs/toolkit";
import { RootState } from "@redux/store";


const initialState = {
    token: ""
};

export const sessionSlice = createSlice({
    name: "session",
    initialState,
    reducers: {
        setSession: (state, action) => {
            state.token = action.payload;
        }
    },
});

// Selector to check if the session is valid
export const hasValidSession = (state : RootState) => state.session.token !== "";

export const { setSession } = sessionSlice.actions;

export default sessionSlice.reducer;