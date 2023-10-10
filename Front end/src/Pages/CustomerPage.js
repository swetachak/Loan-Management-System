import React, { useContext,useState, useEffect } from 'react';
import axios from 'axios';
import { AppContext
 } from '../Context/App.context';
function CustomerPage() {
    const [customer, setCustomer] = useState(null);
    const[cid,setCustid]=useState('');
    const { user,setUser } = useContext(AppContext);

      const[token,setToken]=useState('');
    
    const handleCid =(event) =>{
        setCustid(event.target.value);
    }

    const handleSubmit = () =>{
        setToken(user.token);
        const headers= {"Authorization" : `Bearer ${user.token}`}
        console.log(headers);
        axios.get('https://localhost:7223/api/Customer?id=E0001'+cid,{headers})
        .then(response => setCustomer(response.data));
    }
    

    // useEffect(() => {
    //    // const headers = { 'Authorization': 'Bearer my-token' };
    //     axios.get('https://localhost:7084/api/Customer/getcustbyid?id='+cid)
    //         .then(response => setCustomer(response.data));
    //     }, []);
    // console.log(customer);

    return (
        <div>
            <h3 class="p-3 text-center">React Bearer Token with Axios</h3>
            <h5 className="card-header"> Get Customer Details </h5>
            <div className="card text-center m-3">
                Enter Customer Id: <input type="text" value={cid} onChange={handleCid}/>
                <button onClick={handleSubmit}> Fetch Data </button>
               <h1> Customer Details:</h1>
                {customer && <div className="card-body">Customer ID: {customer?.cid}</div>}
               {customer && <div className="card-body">Customer Name: {customer?.cname}</div>}
               {customer && <div className="card-body">Date Of Joining: {customer?.doj}</div>}
               {customer && <div className="card-body">Address: {customer?.cadd}</div>}
               {customer && <div className="card-body">Balance: {customer?.bal}</div>}
               
               <button onClick={() => {setUser(null) }}> Logout </button>
            </div>
        </div>
    );
}

export default CustomerPage;