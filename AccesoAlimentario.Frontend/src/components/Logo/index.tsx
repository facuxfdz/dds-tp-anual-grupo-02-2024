// material-ui
import { ButtonBase } from '@mui/material';

// project import
import LogoMain from './LogoMain';
import LogoIcon from './LogoIcon';
import Link from "next/link";

// ==============================|| MAIN LOGO ||============================== //

const LogoSection = ({ isIcon, sx, to }:{
    isIcon: boolean,
    sx?: object,
    to: string
}) => (
    <ButtonBase disableRipple component={Link} href={to} sx={sx}>
        {isIcon ? <LogoIcon /> : <LogoMain />}
    </ButtonBase>
);

export default LogoSection;
