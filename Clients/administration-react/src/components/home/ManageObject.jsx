import { useEffect, useState } from "react";


const ManageObject = ({objectToManage, onCreateAdvertisement}) => {
  
  const [activeButton, setActiveButton] = useState('Info')
  const [city, setCity] = useState("")
  const [postalCode, setPostalCode] = useState("")
  const [streetName, setStreetName] = useState("")
  const [streetNumber, setStreetNumber] = useState("")
  const [propertyType, setPropertyType] = useState("")
  const [leaseType, setLeaseType] = useState("")

  useEffect(() => {
    setCity(objectToManage?.city)
    setPostalCode(objectToManage?.postalCode)
    setStreetName(objectToManage?.streetName)
    setStreetNumber(objectToManage?.streetNumber)
    setPropertyType(objectToManage?.propertyType)
    setLeaseType(objectToManage?.leaseType)
  },[objectToManage])
  
  const handleSubmit = (e) => {
    e.preventDefault()
    
    const add = {
      city,
      postalCode,
      streetName,
      streetNumber,
      propertyType,
      leaseType
    }

    onCreateAdvertisement(add)
  }

  

  const handleButtonClick = (e) => {
    setActiveButton(e.target.id)
  }

  const handleCityTextChanged = (e) => {
    setCity(e.target.value)
  }

  const handlePostalCodeTextChanged = (e) => {
    setPostalCode(e.target.value)
  }

  const handleStreetNameTextChanged = (e) => {
    setStreetName(e.target.value)
  }

  const handleStreetNumberTextChanged = (e) => {
    setStreetNumber(e.target.value)
  }

  const handlePropertyTypeSelectChanged = (e) => {
    setPropertyType(e.target.value)
  }

  const handleLeaseTypeSelectChanged = (e) => {
    setLeaseType(e.target.value)
  }

  return (
    <section className="home-sidebar-wrapper" id="right-sidebar">
      <h1 className="home-sidebar-heading">ManageObjects</h1>
      <div className="manage-objects-nav-button-container">
        <button
          onClick={handleButtonClick}
          id="Info"
          className={activeButton === "Info" ? "selected" : ""}
        >
          info
        </button>
        <button
          onClick={handleButtonClick}
          id="Create"
          className={activeButton === "Create" ? "selected" : ""}
        >
          Create
        </button>
        <button
          onClick={handleButtonClick}
          id="Customizie"
          className={activeButton === "Customizie" ? "selected" : ""}
        >
          Customize
        </button>
      </div>
      <form action="" onSubmit={handleSubmit} className="manage-objects-form-wrapper">
        <h1 className="manage-objects-form-heading">Address</h1>
        <div className="manage-objects-form-input-wrapper">
          <label htmlFor="manage-objects-form-input">city</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleCityTextChanged}
            value={city}
            type="text"
            className="manage-objects-form-input"
          />
        </div>
        <div className="manage-objects-form-input-wrapper">
          <label htmlFor="manage-objects-form-input">postal code</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handlePostalCodeTextChanged}
            value={postalCode}
            type="text"
            className="manage-objects-form-input"
          />
        </div>
        <div className="manage-objects-form-input-wrapper">
          <label htmlFor="manage-objects-form-input">street name</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleStreetNameTextChanged}
            value={streetName}
            type="text"
            className="manage-objects-form-input"
          />
        </div>
        <div className="manage-objects-form-input-wrapper">
          <label htmlFor="manage-objects-form-input">street number</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleStreetNumberTextChanged}
            value={streetNumber}
            type="text"
            className="manage-objects-form-input"
          />
        </div>
        <h1 className="manage-objects-form-heading">Propertytype</h1>
        <div className="manage-objects-form-input-wrapper">
          <select
            disabled={activeButton === "Info"}
            onChange={handlePropertyTypeSelectChanged}
            value={propertyType}
            className="manage-objects-form-input"
          >
            <option disabled defaultValue value>
              {" "}
              -- select an option --{" "}
            </option>
            <option value="Villa">Villa</option>
            <option value="Radhus">Radhus</option>
          </select>
        </div>
        <h1 className="manage-objects-form-heading">Leasetype</h1>
        <div className="manage-objects-form-input-wrapper">
          <select
            disabled={activeButton === "Info"}
            onChange={handleLeaseTypeSelectChanged}
            value={leaseType}
            className="manage-objects-form-input"
          >
            <option disabled defaultValue value>
              {" "}
              -- select an option --{" "}
            </option>
            <option value="Egenrätt">Egenrätt</option>
            <option value="Bostadsrätt">Bostadsrätt</option>
          </select>
        </div>
        {activeButton === "Info" || (
          <button 
            className="manage-objects-form-button"
            
          >
            {activeButton === "Create" ? "Create" : "Accept changes"}
          </button>
        )}
      </form>
    </section>
  );
}
export default ManageObject;