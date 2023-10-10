import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import Button from '@mui/material/Button';
import { AppContext } from '../Context/App.context';

function UserItemsPage() {
    const [itemData, setItemData] = useState([]);
    const cid = sessionStorage.getItem("Cid");
    const { user, setUser } = useContext(AppContext);
    const columnNames = ['Issue ID','Item Description', 'Item Make', 'Item Category','Item Valuation'];
    const handleSubmit = () => {
        console.log(cid);
        axios
            .get('https://localhost:7223/api/EmployeeManagement/DisplayAllItemsPurchasedByID/' + cid)
            .then((response) => setItemData(response.data));
        if(itemData===null) {
            alert("No data found!");
        }
        console.log(itemData);
    };
    return(
        <div>
            <div className="card text-center m-3">
                <Button onClick={handleSubmit}> Fetch Items Data </Button>
                <table>
                    <thead>    
                        <tr>
                            {columnNames.map((columnName)=>(
                                <th key = {columnName}>{columnName}</th>
                            ))}    
                        </tr>
                    </thead>

                    <tbody>
                    {itemData.map((item, index) => (
                    <tr key={index}>
                        <td>{item?.issueId}</td>
                        <td>{item?.itemDescription}</td>
                        <td>{item?.itemMake}</td>
                        <td>{item?.itemCategory}</td>
                        <td>{item?.itemValuation}</td>        
                    </tr>
                    ))}
                    </tbody>
                    </table>
                    
            </div>
            <button onClick={() => { setUser(null) }}> Logout </button>
        </div>
    );
}
export default UserItemsPage;