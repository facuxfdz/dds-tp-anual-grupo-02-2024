import React, {useEffect, useState} from 'react';

// material-ui
import {styled, useTheme} from '@mui/material/styles';
import {
    Collapse,
    ClickAwayListener,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Paper,
    Popper,
    Typography,
    alpha, Icon
} from '@mui/material';

// project import
import NavItem from './NavItem';

import {useAppSelector} from "@redux/hook";
import SimpleBarScroll from "@components/Third-Party/SimpleBarScroll";
import Transitions from "@components/@extended/Transitions";
import {IMenuItem} from "@utils/getMenuItems";

// mini-menu - wrapper
const PopperStyled = styled(Popper)(({theme}) => ({
    overflow: 'visible',
    zIndex: 1202,
    minWidth: 180,
    '&:before': {
        content: '""',
        display: 'block',
        position: 'absolute',
        top: 38,
        left: -5,
        width: 10,
        height: 10,
        backgroundColor: theme.palette.background.paper,
        transform: 'translateY(-50%) rotate(45deg)',
        zIndex: 120,
        borderLeft: `1px solid #d3d8db`,
        borderBottom: `1px solid #d3d8db`
    }
}));

// ==============================|| NAVIGATION - LIST COLLAPSE ||============================== //

const NavCollapse = ({menu, level}:
                         {
                             menu: IMenuItem;
                             level: number;
                         }) => {
    const theme = useTheme();

    const drawerOpen = useAppSelector((state) => state.theme.drawerOpen);

    const [open, setOpen] = useState(false);
    const [selected, setSelected] = useState<string | null>(null);

    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        setAnchorEl(null);
        if (drawerOpen) {
            setOpen(!open);
            setSelected(!selected ? menu.id : null);
        } else {
            setAnchorEl(event?.currentTarget);
        }
    };

    const handlerIconLink = () => {
        if (!drawerOpen) {
            setSelected(menu.id);
        }
    };

    const miniMenuOpened = Boolean(anchorEl);

    const handleClose = () => {
        setOpen(false);
        if (!miniMenuOpened) {
            if (!menu.url) {
                setSelected(null);
            }
        }
        setAnchorEl(null);
    };

    useEffect(() => {
        setOpen(false);
        if (!miniMenuOpened) {
            setSelected(null);
        }
        if (miniMenuOpened) setAnchorEl(null);

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [menu.children]);

    const navCollapse = menu.children?.map((item: IMenuItem) => {
        switch (item.type) {
            case 'collapse':
                return (
                    <NavCollapse
                        key={item.id}
                        menu={item}
                        level={level + 1}
                    />
                );
            case 'item':
                return <NavItem key={item.id} item={item} level={level + 1}/>;
            default:
                return (
                    <Typography key={item.id} variant="h6" color="error" align="center">
                        Fix - Collapse or Item
                    </Typography>
                );
        }
    });

    const borderIcon = level === 1 ?
        <i className="fa-duotone fa-solid fa-border-outer" style={{fontSize: '1rem'}}/> : false;
    const menuIcon = menu.icon ?
        <Icon style={{fontSize: drawerOpen ? '1rem' : '1.25rem', overflow: 'visible'}} className={menu.icon}/> : borderIcon;
    const textColor = theme.palette.mode === 'dark' ? 'grey.400' : 'text.primary';
    const iconSelectedColor = theme.palette.mode === 'dark' && drawerOpen ? theme.palette.text.primary : theme.palette.primary.main;

    return (
        <>
            <ListItemButton
                disableRipple
                selected={selected === menu.id}
                {...(!drawerOpen && {onMouseEnter: handleClick, onMouseLeave: handleClose})}
                onClick={handleClick}
                sx={{
                    pl: drawerOpen ? `${level * 28}px` : 1.5,
                    py: !drawerOpen && level === 1 ? 1.25 : 1,
                    ...(drawerOpen && {
                        '&:hover': {
                            bgcolor: theme.palette.mode === 'dark' ? 'divider' : 'primary.lighter'
                        },
                        '&.Mui-selected': {
                            bgcolor: 'transparent',
                            color: iconSelectedColor,
                            '&:hover': {
                                color: iconSelectedColor,
                                bgcolor: theme.palette.mode === 'dark' ? 'divider' : 'transparent'
                            }
                        }
                    }),
                    ...(!drawerOpen && {
                        '&:hover': {
                            bgcolor: 'transparent'
                        },
                        '&.Mui-selected': {
                            '&:hover': {
                                bgcolor: 'transparent'
                            },
                            bgcolor: 'transparent'
                        }
                    })
                }}
            >
                {menuIcon && (
                    <ListItemIcon
                        onClick={handlerIconLink}
                        sx={{
                            minWidth: 28,
                            color: selected === menu.id ? 'primary.main' : textColor,
                            ...(!drawerOpen && {
                                borderRadius: 1.5,
                                width: 36,
                                height: 36,
                                alignItems: 'center',
                                justifyContent: 'center',
                                '&:hover': {
                                    bgcolor: theme.palette.mode === 'dark' ? 'secondary.light' : 'secondary.lighter'
                                }
                            }),
                            ...(!drawerOpen &&
                                selected === menu.id && {
                                    bgcolor: theme.palette.mode === 'dark' ? 'primary.900' : 'primary.lighter',
                                    '&:hover': {
                                        bgcolor: theme.palette.mode === 'dark' ? 'primary.darker' : 'primary.lighter'
                                    }
                                })
                        }}
                    >
                        {menuIcon}
                    </ListItemIcon>
                )}
                {(drawerOpen || (!drawerOpen && level !== 1)) && (
                    <ListItemText
                        primary={
                            <Typography variant="h6" color={selected === menu.id ? 'primary' : textColor}>
                                {menu.title}
                            </Typography>
                        }
                        secondary={
                            menu.caption && (
                                <Typography variant="caption" color="secondary">
                                    {menu.caption}
                                </Typography>
                            )
                        }
                    />
                )}
                {(drawerOpen || (!drawerOpen && level !== 1)) &&
                    (miniMenuOpened || open ? (
                        <i className="fa-solid fa-border-top"
                           style={{fontSize: '0.625rem', marginLeft: 1, color: theme.palette.primary.main}}/>
                    ) : (
                        <i className="fa-solid fa-border-bottom" style={{fontSize: '0.625rem', marginLeft: 1}}/>
                    ))}

                {!drawerOpen && (
                    <PopperStyled
                        open={miniMenuOpened}
                        anchorEl={anchorEl}
                        placement="right-start"
                        style={{
                            zIndex: 2001
                        }}
                        popperOptions={{
                            modifiers: [
                                {
                                    name: 'offset',
                                    options: {
                                        offset: [-12, 1]
                                    }
                                }
                            ]
                        }}
                    >
                        {({TransitionProps}) => (
                            <Transitions in={miniMenuOpened} {...TransitionProps}>
                                <Paper
                                    sx={{
                                        overflow: 'hidden',
                                        mt: 1.5,
                                        boxShadow: theme.palette.mode === 'dark'
                                            ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)`
                                            : `0px 1px 4px ${alpha(theme.palette.grey[900], 0.08)}`,
                                        backgroundImage: 'none',
                                        border: `1px solid ${theme.palette.divider}`
                                    }}
                                >
                                    <ClickAwayListener onClickAway={handleClose}>
                                        <SimpleBarScroll
                                            sx={{
                                                overflowX: 'hidden',
                                                overflowY: 'auto',
                                                maxHeight: 'calc(100vh - 170px)'
                                            }}
                                        >
                                            {navCollapse}
                                        </SimpleBarScroll>
                                    </ClickAwayListener>
                                </Paper>
                            </Transitions>
                        )}
                    </PopperStyled>
                )}
            </ListItemButton>
            {drawerOpen && (
                <Collapse in={open} timeout="auto" unmountOnExit>
                    <List sx={{p: 0}}>{navCollapse}</List>
                </Collapse>
            )}
        </>
    );
};

export default NavCollapse;
