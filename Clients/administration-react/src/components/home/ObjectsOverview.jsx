import { useState } from 'react';
import { faMagnifyingGlass }  from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect } from 'react';
import styles from './ObjectsOverview.module.css'


const ObjectsOverview = ({selectObject, advertisements,onFetchAdvertisements, selectedRowIndex}) => {
  const [selectedIndex, setSelectedIndex] = useState(selectedRowIndex)
  const [searchAddress, setSearchAddress] = useState('')

  useEffect(() =>{
    onFetchAdvertisements()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  },[])
  console.log('RUN')
  const handleSearchAddressChanged = (e) => {
    setSearchAddress(e.target.value)
  }
  const handleRowClicked = (rowObject, index) => {
    selectObject(rowObject)
    setSelectedIndex(index)
  }

  const getAddFullAddress = (add) => {
    return `${add?.city} ${add?.streetName} ${add?.streetNumber}`.toLowerCase()
  }

  return (
    <section className={styles["objects-overview-container"]}>
      <section className={styles["objects-overview-header-container"]}>
        <h1 className={styles["objects-overview-heading"]} >Advertisements <span className={styles["add-counter"]}>{advertisements.filter(add => getAddFullAddress(add)?.includes(searchAddress?.toLowerCase())).length}</span></h1>
        <div className={styles["search-container"]}>
          <FontAwesomeIcon icon={faMagnifyingGlass}/>
          <input placeholder='Search address' value={searchAddress} onChange={handleSearchAddressChanged} className={styles["search"]} type="text" />
        </div>
      </section>
      <section className={styles["objects-overview-table-container"]}>
        <table className={styles["objects-overview-table"]}>
          <thead>
            <tr className={styles["objects-overview-table-top"]}>
              <th id={`${styles["table-top-column-address"]}`}>Address</th>
              <th id={`${styles["table-top-column-type"]}`}>Type</th>
              <th id={`${styles["table-top-column-size"]}`}>Size</th>
              <th id={`${styles["table-top-column-price"]}`}>Price</th>
              <th>Deleted</th>
            </tr>
          </thead>
          <tbody>
          {advertisements.filter(add => getAddFullAddress(add)?.includes(searchAddress?.toLowerCase())).map((add, index) => (
            <tr key={index} onClick={() =>{handleRowClicked(add, index)}} className={selectedIndex === index ? styles["selected"]:""}>
              <td>
                <div className={styles["table-column"]}>{add.streetName} {add.streetNumber}</div>
                <div className={`${styles["table-column-subrow"]} ${styles["table-column"]}`}>{add.city}</div>
              </td>
              <td>
                <div className={styles["table-column"]}>{add.propertyType}</div>
                <div className={`${styles["table-column-subrow"]} ${styles["table-column"]}`}>{add.leaseType}</div>
              </td>
              <td>
                <div className={styles["table-column"]}>{add.area} m2</div>
              </td>
              <td className={styles["table-column"]}>{add.listPrice} kr</td>
              <td className={styles["table-column"]}>{add.deleted.toString()}</td>
            </tr>
          ))}
          </tbody>
        </table>
      </section>
    </section>
  )
}
export default ObjectsOverview;