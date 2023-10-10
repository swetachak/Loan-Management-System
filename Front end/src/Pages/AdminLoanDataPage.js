import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import Button from '@mui/material/Button';
import { AppContext } from '../Context/App.context';

function AdminLoanDataPage() {
    const [deleteLoanId, setDeleteLoanId] = useState('');
    const [newType, setNewType]  =useState('');
    const [newDuration, setNewDuration]= useState('');
    const [newLoanObj, setNewLoanObj] = useState({loanType: '', durationInYears: ''});
    const [loandata, setloandata] = useState([]);
    const [AllLoanData, setAllLoanData] = useState([]);
    const [cid, setCustid] = useState('');
    const { user, setUser } = useContext(AppContext);
    const[Error, setError] = useState(false);
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [icategoryOptions, setIcategoryOptions] = useState([]); // State to store category options
    const [token, setToken] = useState('');
    const [loanEditObj,setloanEditObj] = useState({loanId: '', loanType: '', durationInYears: ''});
    const[loanId,setLoanId] = useState('');
    const[loanType,setLoanType] = useState('');
    const [durationInYears, setDurationInYears] = useState('');
    const loanTypeNavigation = null;

    const handleNewType = (event) =>{
        setNewType(event.target.value);
    }
    const handleNewDuration = (event) =>{
        setNewDuration(event.target.value);
    }
    const handleNewLoan  = async (event) =>{
        newLoanObj.loanType = loanType;
        newLoanObj.durationInYears = newDuration;
        event.preventDefault();
        try{
            const response=await axios
                .post('https://localhost:7223/api/AdminLoanCardManagement/AddLoanCard',
                newLoanObj
                )
            
            console.log(response.data);
           
        }
        catch(error){
            setError(true);
        }
  
    
    }
    
    const handleLoandata = async(event) => {
        await axios
            .get('https://localhost:7223/api/AdminLoanCardManagement/GetAllLoans')
            .then((result) => 
            setAllLoanData(result.data));
        console.log(AllLoanData);
            
    };
    const handleLoanId =(event) => {
        setLoanId(event.target.value);
    }
    const handleLoanType =(event) => {
        setLoanType(event.target.value);
    }
    const handleLoanDuration =(event) => {
        setDurationInYears(event.target.value);
    }
    const handleEditLoan = async(_loanId)=>{

        try
        {
            const loanToEdit = AllLoanData.find((loanRecord) => loanRecord.loanId == _loanId);
            setloanEditObj({
                loanId: loanToEdit.loanId, 
                loanType: loanToEdit.loanType, 
                durationInYears: loanToEdit.durationInYears});
            setLoanId(loanToEdit.loanId);
            setIsFormOpen(true);
        }
        catch (error) {
            console.error('Error editing loan:', error);
          }
        
    };
    const handleSubmitEditLoan = async(event)=>{
        loanEditObj.loanId = loanId;
        loanEditObj.loanType = loanType;
        loanEditObj.durationInYears = durationInYears;

        event.preventDefault();
        try{
            console.log(loanEditObj);
            const response=await axios
                .put('https://localhost:7223/api/AdminLoanCardManagement/UpdateLoanCard/' + loanId,
                loanEditObj
                )
                alert(response.data);
        }
        catch(error){
            setError(true);
        }
        setIsFormOpen(false);
    }

    const handleDeleteLoanId =(event) => {
        setDeleteLoanId(event.target.value);
    }
    const handleDeleteLoan = async (_loanId) => {
        
        axios
            .delete('https://localhost:7223/api/AdminLoanCardManagement/DeleteLoanCard/' + _loanId)
            .then(result => {console.log("deleted successfully")
        })
    
    }
        // Use useEffect to fetch category options when the component mounts
        useEffect(() => {
            // Make an API call to fetch category options
            // Replace 'YOUR_CATEGORY_API_ENDPOINT' with your actual API endpoint
            axios.get('https://localhost:7223/api/Category/Categories')
                .then((response) => {
                    // Assuming the response data is an array of category options
                    const categories = response.data;
                    console.log(response);
                    setIcategoryOptions(categories);
                    if(categories.length==1){setLoanType(categories[0])};
                })
                .catch((error) => {
                    console.error("Error fetching category options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }, []); // Run this effect only once when the component mounts

        useEffect(() => {

            console.log(AllLoanData);
        },[AllLoanData]);

    return (
        <div>
            <div className="card text-center m-3">
                <h1>Loan Data Management</h1>
                <h3>Add New Loan Data </h3>
                <form onSubmit={handleNewLoan}>
                <div>
                    Loan Type:
                    <select value={loanType} onChange={handleLoanType}>
                        <option value="" disabled>Select an Item Category</option>
                        {icategoryOptions.map((option) => (
                            <option key={option} value={option}>{option}</option>
                        ))}
                    </select>
                </div>
                    <div>
                        Loan Duration: <input type="text" onChange={handleNewDuration} />
                    </div>
                    <div>
                        <button type = "submit"> Add New Loan </button>
                    </div>
                </form>                    
                <br></br>     
                <button onClick={handleLoandata}>Get Loan data for all Customers </button> 
                {AllLoanData.map((AllLoanData, index) => (
                    <div key={index}>
                        <div className="card-body">Loan ID: {AllLoanData?.loanId}</div>
                        <div className="card-body">Loan Type: {AllLoanData?.loanType}</div>
                        <div className="card-body">
                            Loan Duration: {AllLoanData?.durationInYears}
                        </div>
                        <div> <Button className="btn1" onClick={() => handleEditLoan(AllLoanData?.loanId)}>Edit Loans</Button> 
                        {isFormOpen &&(
                            <form onSubmit={handleSubmitEditLoan}>
                                <div>
                                    Loan ID: <input type="text" value={loanId} readOnly />
                                </div>
                                <div>
                    Loan Type:
                    <select value={loanType} onChange={handleLoanType}>
                        <option value="" disabled>Select an Item Category</option>
                        {icategoryOptions.map((option) => (
                            <option key={option} value={option}>{option}</option>
                        ))}
                    </select>
                </div>
                                <div>
                                    Loan Duration: <input type="text" value={durationInYears} onChange={handleLoanDuration} />
                                </div>
                                <div>
                                    <button type = "submit"> Edit Details </button>
                                </div>
                            </form>

                        )}  
                            
                        </div>

                        <Button className="btn1" onClick={() => handleDeleteLoan(AllLoanData.loanId)}>
              Delete Loan
            </Button>
                        <br></br>
                    </div>
                ))}
                <button onClick={() => { setUser(null) }}> Logout </button>
            </div>
        </div>
    );
}
export default AdminLoanDataPage;