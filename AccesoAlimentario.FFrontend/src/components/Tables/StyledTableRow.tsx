"use client";
import {styled} from "@mui/material/styles";
import {TableRow} from "@mui/material";

export const StyledTableRow = styled(TableRow)(() => ({
    '&:last-of-type td, &:last-of-type th': {
        border: 0
    }
}));