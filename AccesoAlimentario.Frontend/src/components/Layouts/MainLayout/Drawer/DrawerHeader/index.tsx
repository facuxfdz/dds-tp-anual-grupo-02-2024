// project import
import DrawerHeaderStyled from './DrawerHeaderStyled';
import {Box} from "@mui/material";
import {useAppSelector} from "@redux/hook";

// ==============================|| DRAWER HEADER ||============================== //

export default function DrawerHeader({open}: {
    open: boolean;
}) {
    const customization = useAppSelector((state) => state.customization);

    return (
        <DrawerHeaderStyled open={open} sx={{
            minHeight: '60px',
            width: 'inherit',
            paddingTop: '8px',
            paddingBottom: '8px',
            paddingLeft: open ? '24px' : 0
        }}>
            {open ? <Box
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        height: 45,
                        width: "auto",
                        ml: 4,
                    }}
                    component={"img"}
                    src={customization.logo}
                    alt="logo"
                />
                :
                <Box
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        height: 35,
                        width: "auto",
                    }}
                    component={"img"}
                    src={customization.icon}
                    alt="icon"
                />
            }
        </DrawerHeaderStyled>
    );
}