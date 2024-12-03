import {parseISO, format as formatFns} from "date-fns";

export const formatDate = (date: string, format: string = "yyyy-MM-dd") => {
    return formatFns(parseISO(`${date}Z`), format);
}