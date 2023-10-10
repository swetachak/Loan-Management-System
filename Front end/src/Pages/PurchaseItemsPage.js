import axios from 'axios';
import React, { useContext,useState, useEffect } from 'react';
import { AppContext } from '../Context/App.context';

const PurchaseItemsPage = () => {
    const { user, setUser } = useContext(AppContext);
    const [eid, setEid] = useState(null);
    const [icategoryOptions, setIcategoryOptions] = useState([]); // State to store category options
    const [icategory, setIcategory] = useState(null);
    const [imakeOptions, setImakeOptions] = useState([]);
    const [imake, setImake] = useState(null);
    const [tenureOptions, setTenureOptions] = useState([]);
    const [itenure, setTenure] = useState(10);
    const [desc , setDesc] = useState(null);
    const [ivalue, setIvalue] = useState('');;

    // Use useEffect to update eid when user changes
    useEffect(() => {
        if (user) {
            setEid(user.employee_Id);
        } else {
            setEid(null);
        }
    }, [user]);

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
                    if(categories.length==1){setIcategory(categories[0])};
                })
                .catch((error) => {
                    console.error("Error fetching category options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }, []); // Run this effect only once when the component mounts

    // Use useEffect to fetch imake options based on selected icategory
    useEffect(() => {
        // Check if icategory is selected
        if (icategory) {
            // Make an API call to fetch imake options based on icategory
            // Replace 'YOUR_IMAKE_API_ENDPOINT' with your actual API endpoint
            axios.get('https://localhost:7223/api/Category/Materials/'+icategory)
                .then((response) => {
                    // Assuming the response data is an array of imake options
                    const imakes = response.data;
                    console.log(imakes);
                    setImakeOptions(imakes);
                    if(imakes.length==1){setImake(imakes[0])};
                })
                .catch((error) => {
                    console.error("Error fetching imake options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }
    }, [icategory]); // Run this effect when icategory changes

    // Use useEffect to fetch tenure options based on selected icategory and imake
    useEffect(() => {
        // Check if icategory is selected
        if (icategory) {
            // Make an API call to fetch tenure options based on icategory and imake
            // Replace 'YOUR_TENURE_API_ENDPOINT' with your actual API endpoint
            axios.get('https://localhost:7223/api/Category/Tenures/'+icategory)
                .then((response) => {
                    // Assuming the response data is an array of tenure options
                    const tenures = response.data;
                    console.log(tenures);
                    setTenureOptions(tenures);
                    if(tenures.length==1){setTenure(tenures[0])};
                })
                .catch((error) => {
                    console.error("Error fetching tenure options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }
    }, [icategory]); // Run this effect when icategory or imake changes

    const handleEid =(event) =>{
        setEid(event.target.value);
    }
    const handleIcategory =(event) =>{
        setIcategory(event.target.value);
    }
    const handleDesc =(event) =>{
        setDesc(event.target.value);
    }
    const handleIvalue =(event) =>{
        setIvalue(event.target.value);
    }
    const handleImake =(event) =>{
        setImake(event.target.value);
    }
    const handleTenure = (event) => {
        console.log("handleTenure " + event.target.value);
        const parsedTenure = parseInt(event.target.value, 10);
        setTenure(isNaN(parsedTenure) ? null : parsedTenure); // Handle NaN case
    }
    
    const handleApply = async (event) => {
        event.preventDefault(); // Prevent the default form submission

        try {

            // Create an object with the form data
            const formData = {
                itemCategory: icategory,
                itemDescription: desc,
                itemValue: ivalue,
                itemMake: imake,
                tenure : itenure,
            };

            // Post Request
            console.log(formData)
            const response = await axios.post(
                'https://localhost:7223/api/EmployeeApplyForLoan/ApplyForLoan/'+eid,
                formData
            );

            
            alert("Loan application submitted successfully!");

            // reset the component state as needed
            setIcategory(null);
            setDesc(null);
            setIvalue(null);
            setImake(null);
            setTenure(null);
        } catch (error) {
            // Handle any errors (e.g., display an error message)
            console.error("Error submitting loan application:", error);
            alert("Error submitting loan application. Please try again later.");
        }
    };
    return (
        <div>
            <h2>Select Product and Apply for Loan</h2>
           <form onSubmit={handleApply}>
                <div>
                    EmployeeID: <input type="text" value={eid} readOnly />
                </div>
                <div>
                    Item Category:
                    <select value={icategory} onChange={handleIcategory}>
                        <option value="" disabled>Select an Item Category</option>
                        {icategoryOptions.map((option) => (
                            <option key={option} value={option}>{option}</option>
                        ))}
                    </select>
                </div>
                <div>
                    Item Description: <input type="text" value={desc} onChange={handleDesc} />
                </div>
                <div>
                   Item Value: <input type="text" value={ivalue} onChange={handleIvalue} />
                </div>
                <div>
                    Item Make:
                    <select value={imake} onChange={handleImake}>
                        <option value="" disabled>Select an Item Make</option>
                        {imakeOptions.length === 0 ? (
            <option disabled>Loading...</option>
        ) : (
            imakeOptions.map((option) => (
                <option key={option} value={option}>{option}</option>
            ))
        )}
                    </select>
                </div>
                <div>
                    Tenure:
                    <select value={itenure} onChange={handleTenure}>
                        <option value="" disabled>Select a Tenure</option>
                        {tenureOptions.length === 0 ? (
            <option disabled>Loading...</option>
        ) : (
            tenureOptions.map((option) => (
                <option key={option} value={option}>{option}</option>
            ))
        )}
                    </select>
                </div>
                <div>
                    <button type="submit"> Apply Loan </button>
                </div>
               {/* {Error && <div>Invalid Details </div>} */}
            </form> 
            <button onClick={() => { setUser(null) }}> Logout </button>
        </div>
    )
}
export default PurchaseItemsPage;