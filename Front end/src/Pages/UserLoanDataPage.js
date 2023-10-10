import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import { AppContext } from '../Context/App.context';
import Button from '@mui/material/Button';

function UserLoanDataPage() {
    const [loandata, setloandata] = useState([]);
    const {user,setUser} = useContext(AppContext);
    const cid = sessionStorage.getItem("Cid");
    const columnNames = ['Loan ID', 'Loan Type', 'Loan Duration'];
    const handleSubmit = () => {
        // setToken(user.token);
        // const headers = { Authorization: `Bearer ${user.token}` };
        // console.log(headers);
        console.log(cid);
        axios
            .get('https://localhost:7223/api/EmployeeManagement/GetAllLoansByID/' + cid)
            .then((response) => setloandata(response.data));
        console.log(loandata);
    };
    
    return (
        <div>
            <div className="card text-center m-3">
                <Button onClick={handleSubmit}> Fetch Loan Data </Button>
                <table>
                    <thead>    
                        <tr>
                            {columnNames.map((columnName)=>(
                                <th key = {columnName}>{columnName}</th>
                            ))}    
                        </tr>
                    </thead>
                    <tbody>
                    {loandata.map((loan, index) => (
                    <tr key={index}>
                        <td>{loan?.loanId}</td>
                        <td>{loan?.loanType}</td>
                        <td>{loan?.durationInYears}</td>      
                    </tr>
                    ))}
                    </tbody>
                    </table>
            </div>
            <button onClick={() => { setUser(null) }}> Logout </button>
        </div>
    );
}
export default UserLoanDataPage;