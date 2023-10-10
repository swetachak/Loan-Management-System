import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import Button from '@mui/material/Button';
import { AppContext } from '../Context/App.context';

function AdminItemDataPage() {
    const [itemdata, setItemdata] = useState([]);
    const [AllItemData, setAllItemData] = useState([]);
    const [cid, setCustid] = useState('');
    const { user, setUser } = useContext(AppContext);
    const[Error, setError] = useState(false);
    const [isFormOpen, setIsFormOpen] = useState(false);

    const [token, setToken] = useState('');
    const [itemEditObj,setItemEditObj] = useState({itemId: '', itemDescription: '', issueStatus: '', itemCategory: '',itemValuation : ''});
    const[itemId,setItemId] = useState('');
    const[itemDesc,setItemDesc] = useState('');
    const[issueStatus,setIssueStatus] = useState('');
    const[itemCat,setItemCat] = useState('');
    const[itemValue,setItemValue] = useState('');
    const [itemMake,setItemMake] = useState('');
    const itemCatNavigation = null;
    const itemMakeNavigation = null;
    const [icategoryOptions, setIcategoryOptions] = useState([]); // State to store category options
    const [imakeOptions, setImakeOptions] = useState([]);
    const [deleteItemId, setDeleteItemId] = useState('');

    const handleAllItemsdata = () => {
        console.log("ha yes")
        axios
            .get('https://localhost:7223/api/AdminItemDataManagement')
            .then((result) => setAllItemData(result.data));
        console.log(AllItemData);
    };
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
                    if(categories.length==1){setItemCat(categories[0])};
                })
                .catch((error) => {
                    console.error("Error fetching category options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }, []); // Run this effect only once when the component mounts

    // Use useEffect to fetch imake options based on selected icategory
    useEffect(() => {
        // Check if icategory is selected
        if (itemCat) {
            // Make an API call to fetch imake options based on icategory
            // Replace 'YOUR_IMAKE_API_ENDPOINT' with your actual API endpoint
            axios.get('https://localhost:7223/api/Category/Materials/'+itemCat)
                .then((response) => {
                    // Assuming the response data is an array of imake options
                    const imakes = response.data;
                    console.log(imakes);
                    setImakeOptions(imakes);
                    if(imakes.length==1){setItemMake(imakes[0])};
                })
                .catch((error) => {
                    console.error("Error fetching imake options:", error);
                    // Handle the error appropriately, e.g., display an error message
                });
        }
    }, [itemCat]); // Run this effect when icategory changes

    const handleSubmit = () => {
        setToken(user.token);
        const headers = { Authorization: `Bearer ${user.token}` };
        console.log(headers);
        axios
            .get('https://localhost:7223/api/' + cid)
            .then((response) => setItemdata(response.data));
        console.log(itemdata);
    };
    // const handleItemdata = () => {
    //     axios
    //         .get('https://localhost:7223/api/')
    //         .then((result) => setAllItemData(result.data));
    // };
    const handleItemId =(event) => {
        setItemId(event.target.value);
    }
    const handleItemDesc =(event) => {
        setItemDesc(event.target.value);
    }
    const handleIssueStatus =(event) => {
        setIssueStatus(event.target.value);
    }
    const handleItemCat =(event) => {
        setItemCat(event.target.value);
    }
    const handleItemValue =(event) => {
        setItemValue(event.target.value);
    }
    const handleItemMake =(event) => {
        setItemMake(event.target.value);
    }
    const handleEditItem = (_itemid) => {
        try
        {
            const itemToEdit = AllItemData.find((_item) => _item.itemId == _itemid);
            setItemEditObj(
                {
                    itemId: itemToEdit.itemId, 
                    itemDescription: itemToEdit.itemDescription, 
                    issueStatus: itemToEdit.issueStatus, 
                    itemCategory: itemToEdit.itemCategory,
                    itemValuation : itemToEdit.itemValuation
                }
            );
            setItemId(_itemid);
            console.log(itemEditObj);
            setIsFormOpen(true);
        }
        catch (error) {
            console.error('Error editing item:', error);
          }
    };
    const handleSubmitEditItem = async(event)=>{
        itemEditObj.itemId = itemId;
        itemEditObj.itemDescription = itemDesc;
        itemEditObj.issueStatus = issueStatus;
        itemEditObj.itemCategory = itemCat;
        itemEditObj.itemValuation = itemValue;
        itemEditObj.itemMake = itemMake;
        event.preventDefault();
        console.log(itemEditObj);
        try{
            const response=await axios
                .put('https://localhost:7223/api/AdminItemDataManagement/' + itemId,
                itemEditObj
                )
                alert(response.data);
        }
        catch(error){
            setError(true);
        }
        setIsFormOpen(false);
    }
    const handleDeleteItemId =(event) => {
        setDeleteItemId(event.target.value);
    }
    const handleDeleteItem = (_itemId) => {

        try{
        axios
            .delete('https://localhost:7223/api/AdminItemDataManagement/' + _itemId)
            .then(result => {console.log("deleted successfully")
        })
        alert(" Item Id "+ _itemId+" deleted successfully");
    }
    catch (error) {
        // Handle the error if needed
        console.error('Error deleting item:', error);
    }
}


    const handleNewItem = async (event) => {
        event.preventDefault();
        
        // Prepare the new item object with the form data
        const newItem = {
            itemDescription: itemDesc,
            issueStatus: issueStatus,
            itemCategory: itemCat,
            itemValuation: itemValue,
            itemMake: itemMake
        };

        try {
            // Make a POST request to your API endpoint to create a new item
            console.log(newItem);
            const response = await axios.post('https://localhost:7223/api/AdminItemDataManagement', newItem);

            // Handle the response, e.g., show a success message or reset the form
            alert(response.data);

            // Clear the form inputs
            setItemDesc('');
            setIssueStatus('');
            setItemCat('');
            setItemValue('');
            setItemMake('');
        } catch (error) {
            setError(true);
        }
    };

    return (
        <div>
            <div className="card text-center m-3">
                <h1>Item Data Management</h1>
                <h3>Add New Item Data </h3>
                <form onSubmit={handleNewItem}>
                <div>
                    Item Category:
                    <select value={itemCat} onChange={handleItemCat}>
                        <option value="" disabled>Select an Item Category</option>
                        {icategoryOptions.map((option) => (
                            <option key={option} value={option}>{option}</option>
                        ))}
                    </select>
                </div>
                    <div>
                        Item Description: <input type="text" onChange={handleItemDesc}/>
                    </div>
                    <div>
                        Item Value: <input type="text" onChange={handleItemValue}/>
                    </div>
                    <div>
                        Issue Status:
                        <select onChange={handleIssueStatus}>
                            <option value="issued">issued</option>
                            <option value="waiting">waiting</option>
                            <option value="rejected">rejected</option>
                        </select>
                    </div>
                    <div>
                    Item Make:
                    <select value={itemMake} onChange={handleItemMake}>
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
                        <button type = "submit"> Add New Item </button>
                    </div>
                </form>                    
                <br></br>     
                <button className='btn1' onClick={handleAllItemsdata}>Get All Items Data</button>
                {AllItemData.map((AllItemData, index) => (
                    <div key={index}>
                        <div className="card-body">Item ID: {AllItemData?.itemId}</div>
                        <div className="card-body">Item Description: {AllItemData?.itemDescription}</div>
                        <div className="card-body">Issue Status: {AllItemData?.issueStatus}</div>
                        <div className="card-body">Item Make: {AllItemData?.itemMake}</div>
                        <div className="card-body">Item Category: {AllItemData?.itemCategory}</div>
                        <div className="card-body">Item Valuation: {AllItemData?.itemValuation}</div>

                        <div> <Button className="btn1" onClick={() => handleEditItem(AllItemData.itemId)}>Edit Items</Button> 
                        {isFormOpen &&(
                            <form onSubmit={handleSubmitEditItem}>
                                <div>
                                   Item ID: <input type="text" value={itemId} readOnly />
                                </div>
                                <div>
                    Item Category:
                    <select value={itemCat} onChange={handleItemCat}>
                        <option value="" disabled>Select an Item Category</option>
                        {icategoryOptions.map((option) => (
                            <option key={option} value={option}>{option}</option>
                        ))}
                    </select>
                </div>
                    <div>
                        Item Description: <input type="text" value={itemDesc} onChange={handleItemDesc}/>
                    </div>
                    <div>
                        Item Value: <input type="text" value = {itemValue} onChange={handleItemValue} />
                    </div>
                    <div>
                        Issue Status:
                        <select value = {issueStatus} onChange={handleIssueStatus}>
                            <option value="issued">issued</option>
                            <option value="waiting">waiting</option>
                            <option value="rejected">rejected</option>
                        </select>
                    </div>
                    <div>
                    Item Make:
                    <select value={itemMake} onChange={handleItemMake}>
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
                                    <button type = "submit"> Edit Details </button>
                                </div>
                            </form>

                        )}  

                        </div>

                        <Button className="btn1" onClick={() => handleDeleteItem(AllItemData.itemId)}>
              Delete Item
            </Button>
                        <br></br>
                    </div>
                ))}
                <button onClick={() => { setUser(null) }}> Logout </button>

            </div>

            </div>
    );
}
export default AdminItemDataPage;
