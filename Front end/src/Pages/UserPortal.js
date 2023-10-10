import React, { useContext,useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import { useNavigate } from 'react-router-dom';
import { AppContext } from '../Context/App.context';
import './MyStyle.css';
//const navigate=useNavigate();

function UserPortal()  {
    const { user, setUser } = useContext(AppContext);
    const navigate=useNavigate();
    const handleClick = async (event) =>
    {
    navigate('/Userloandata');
    };

    const handleItemClick = async (event) =>
    {
        navigate('/Useritemsdata');
    };
    const handlePurchaseItem = async (event) =>
    {
        navigate('/purchaseItems');
    };
    

    return(
        <div>
            <div>
        <center>
            <h1 className="title">Welcome to Loan Management Application</h1><br></br>
            <h3 className="title">Customer Dashboard</h3><br></br>
            <Stack direction="row" spacing={2}>
                <Button className="btn1" onClick={handleClick}>View Loans</Button>
                <Button className="btn1" onClick={handlePurchaseItem}> Apply For Loans</Button>
                <Button className="btn1" onClick={handleItemClick}>View Items Purchased</Button>
            </Stack>
            {/*  */}
        </center>
        </div>
        <button onClick={() => { setUser(null) }}> Logout </button>
        
        </div>
    )
}
export default UserPortal;
