import { useEffect, useState } from 'react';
import { faChevronRight, faChevronLeft }  from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import styles from './ManageObject.module.css'


const ManageObject = ({objectToManage, onCreateAdvertisement}) => {
  const [mobileManageObjectVisible, setMobileManageObjectVisible] = useState(false)
  const [activeButton, setActiveButton] = useState('Info')
  const [city, setCity] = useState("")
  const [postalCode, setPostalCode] = useState("")
  const [streetName, setStreetName] = useState("")
  const [streetNumber, setStreetNumber] = useState("")
  const [propertyType, setPropertyType] = useState("")
  const [leaseType, setLeaseType] = useState("")
  const [image, setImage] = useState("")
  const [previewImage, setPreviewImage] = useState("")
  const [displayImage, setDisplayImage] = useState("")
  
  useEffect(() => {
    if (activeButton !== "Create") {
      setCity(objectToManage?.city)
      setPostalCode(objectToManage?.postalCode)
      setStreetName(objectToManage?.streetName)
      setStreetNumber(objectToManage?.streetNumber)
      setPropertyType(objectToManage?.propertyType)
      setLeaseType(objectToManage?.leaseType)
      setDisplayImage(objectToManage?.imageBin)
      setPreviewImage("")
    }
    else {
      setCity("")
      setPostalCode("")
      setStreetName("")
      setStreetNumber("")
      setPropertyType("")
      setLeaseType("")
      setDisplayImage("")
      setPreviewImage("")
    }
  },[objectToManage,activeButton])
  
  
  const toBase64 = (file) => 
    new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => resolve(reader.result)
      reader.onerror = (error) => reject(error)
    })
  
  const handleSubmit = async (e) => {
    e.preventDefault()

    let imageBin = await toBase64(image)

    const add = {
      city,
      postalCode,
      streetName,
      streetNumber,
      propertyType,
      leaseType,
      imageBin
    }
  
    onCreateAdvertisement(add)
  }

  const handleToggleManageObject = () => {
    setMobileManageObjectVisible(!mobileManageObjectVisible)
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

  const handleSelectedImageChanged = (e) => {
    setImage(e.target.files[0])
    let previewBlob = URL.createObjectURL(e.target.files[0])
    setPreviewImage(previewBlob)
  }
  //TODO: add custom validation to form
  return (
    <section className={`home-sidebar-container ${mobileManageObjectVisible ? "":styles["hidden"]}`} id={styles["right-sidebar"]}>
      <h1 className="home-sidebar-heading">ManageObjects</h1>
      <div className={styles["manage-objects-nav-button-container"]}>
        <button
          onClick={handleButtonClick}
          id="Info"
          className={activeButton === "Info" ? styles["selected"] : ""}
        >
          info
        </button>
        <button
          onClick={handleButtonClick}
          id="Create"
          className={activeButton === "Create" ? styles["selected"] : ""}
        >
          Create
        </button>
        <button
          onClick={handleButtonClick}
          id="Customizie"
          className={activeButton === "Customizie" ? styles["selected"] : ""}
        >
          Customize
        </button>
      </div>
      <form action="" onSubmit={handleSubmit} className={styles["manage-objects-form"]}>
        <h1 className={styles["manage-objects-form-heading"]}>Address</h1>
        <div className={styles["manage-objects-form-input-container"]}>
          <label htmlFor="manage-objects-form-input">city</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleCityTextChanged}
            value={city}
            type="text"
            className={styles["manage-objects-form-input"]}
            onInvalid={(e) => e.target.setCustomValidity("City is required")}
            required
          />
        </div>
        <div className={styles["manage-objects-form-input-container"]}>
          <label htmlFor="manage-objects-form-input">postal code</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handlePostalCodeTextChanged}
            value={postalCode}
            type="text"
            className={styles["manage-objects-form-input"]}
            required
            onInvalid={(e) => e.target.setCustomValidity("Postal code is required")}
          />
        </div>
        <div className={styles["manage-objects-form-input-container"]}>
          <label htmlFor="manage-objects-form-input">street name</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleStreetNameTextChanged}
            value={streetName}
            type="text"
            className={styles["manage-objects-form-input"]}
            required
            onInvalid={(e) => e.target.setCustomValidity("Street name is required")}
          />
        </div>
        <div className={styles["manage-objects-form-input-container"]}>
          <label htmlFor="manage-objects-form-input">street number</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleStreetNumberTextChanged}
            value={streetNumber}
            type="text"
            className={styles["manage-objects-form-input"]}
            required
            onInvalid={(e) => e.target.setCustomValidity("Street number is required")}
          />
        </div>
        <h1 className={styles["manage-objects-form-heading"]}>Propertytype</h1>
        <div className={styles["manage-objects-form-input-container"]}>
          <select
            disabled={activeButton === "Info"}
            onChange={handlePropertyTypeSelectChanged}
            value={propertyType}
            className={styles["manage-objects-form-input"]}
            required
            onInvalid={(e) => e.target.setCustomValidity("Propertytype is required")}
          >
            <option value="" defaultValue disabled hidden>
              -- select an option --
            </option>
            <option value="Villa">Villa</option>
            <option value="Radhus">Radhus</option>
          </select>
        </div>
        <h1 className={styles["manage-objects-form-heading"]}>Leasetype</h1>
        <div className={styles["manage-objects-form-input-container"]}>
          <select
            disabled={activeButton === "Info"}
            onChange={handleLeaseTypeSelectChanged}
            value={leaseType}
            className={styles["manage-objects-form-input"]}
            required
            onInvalid={(e) => e.target.setCustomValidity("Leasetype is required")}
          >
            <option value="" defaultValue disabled hidden>
              -- select an option --
            </option>
            <option value="Egenrätt">Egenrätt</option>
            <option value="Bostadsrätt">Bostadsrätt</option>
          </select>
        </div>
        <div className={styles["manage-objects-form-input-container"]} id={styles["image-input-container"]}>
          <label htmlFor="manage-objects-form-input">image</label>
          <input
            disabled={activeButton === "Info"}
            onChange={handleSelectedImageChanged}
            type="file"
            className={styles["manage-objects-form-input"]}
          />
          {(activeButton !== "Create" && displayImage) && (
            <img src={displayImage} className={styles["form-image"]} height={mobileManageObjectVisible ? "140":"250"} width={mobileManageObjectVisible ? "140":"250"}/>
          )}
          {(activeButton === "Create" && previewImage) && (
            <img src={previewImage} className={styles["form-image"]} height={mobileManageObjectVisible ? "140":"250"} width={mobileManageObjectVisible ? "140":"250"}/>
          )}
        </div>
        {activeButton === "Info" || (
          <button 
            className={styles["manage-objects-form-button"]}
            
          >
            {activeButton === "Create" ? "Create" : "Accept changes"}
          </button>
        )}
      </form>
    <FontAwesomeIcon onClick={handleToggleManageObject} icon={mobileManageObjectVisible ? faChevronRight:faChevronLeft} className={styles["right-sidebar-toggle-button"]}/>
    </section>
  );
}
export default ManageObject;