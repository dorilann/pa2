import {AppBar, Badge, IconButton, Toolbar} from "@mui/material";
import {Link, useNavigate} from "react-router-dom";
import {Stack} from "@mui/system";
import LogoDevIcon from '@mui/icons-material/LogoDev';
import LoginIcon from '@mui/icons-material/Login'
import LogoutIcon from '@mui/icons-material/Logout';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import {useEffect, useState} from "react";
import {axiosInstance} from "../../../../api/axios";

function NavBarIcons(props: { sx: { border: string; margin: string; borderBlockColor: string; borderStyle: string }, onClick1: () => void }) {
    return (
    <Stack direction="row" spacing={1} sx={{marginLeft: "auto"}} alignItems="revert">

        <Link to="/profile">
            <IconButton sx={{margin: "0"}} size="large" edge="start" color="inherit"
                        aria-label="logo">
                <AccountCircleIcon/>
            </IconButton>
        </Link>
       
        <Link to="/login">
            <IconButton sx={props.sx} size="large" edge="start" color="inherit"
                        aria-label="logo">
                <LoginIcon/>
            </IconButton>
        </Link>
            <IconButton sx={props.sx} size="large" edge="start" color="inherit" aria-label="logo"
                        onClick={props.onClick1}>
            <LogoutIcon/>
        </IconButton>
      


    </Stack>
    );
}

const NavBar = () => {

    const handleClick = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("id")
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("id");
    }

    const iconStyle = {border: "GrayText", borderBlockColor: "Menu", borderStyle: "solid", margin: "0"}


    return (
        <div className="navbar">
            <div className="content">
                <AppBar position="sticky" elevation={0} style={{backgroundColor: "#d6d4fc", padding: "0"}}>
                    <Toolbar sx={{display: "flex", alignContent: "center"}}>

                        <Link to='/'>
                            <img src="https://imgur.com/QDpfmD5.png" alt="logo"/>
                        </Link>

                        <NavBarIcons sx={iconStyle} onClick1={handleClick}/>
                    </Toolbar>
                </AppBar>
            </div>
        </div>
    );
}
export {NavBar}