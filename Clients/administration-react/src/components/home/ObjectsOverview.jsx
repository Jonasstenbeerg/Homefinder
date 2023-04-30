import { faMagnifyingGlass }  from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect } from 'react';


const ObjectsOverview = ({selectObject, advertisements,onFetchAdvertisements}) => {

  useEffect(() =>{
    onFetchAdvertisements()
  },[])
  
  const handleRowClicked = (rowObject) => {
    selectObject(rowObject)
  }

  return (
    <section className="objects-overview-wrapper">
      <section className="objects-overview-header-container">
        <h1 className="objects-overview-heading" >Advertisements <span className='add-counter'>{advertisements.length}</span></h1>
        <div className='search-container'>
          <FontAwesomeIcon icon={faMagnifyingGlass}/>
          <input placeholder='search address' className='search' type="text" />
        </div>
      </section>
      <section className="objects-overview-table-container">
        <table className='objects-overview-table'>
          <thead>
            <tr className='objects-overview-table-top'>
              <th>Address</th>
              <th>Type</th>
              <th>Size</th>
              <th>price</th>
              <th>deleted</th>
            </tr>
          </thead>
          <tbody>
          {advertisements.map((add, index) => (
            <tr key={index} onClick={() =>{handleRowClicked(add)}}>
              <td>
                <div className='table-column'>{add.streetName} {add.streetNumber}</div>
                <div className='table-column-subrow table-column'>{add.city}</div>
              </td>
              <td>
                <div className='table-column'>{add.propertyType}</div>
                <div className='table-column-subrow table-column'>{add.leaseType}</div>
              </td>
              <td>
                <div className='table-column'>{add.area}m2</div>
                
              </td>
              <td className='table-column'>{add.listPrice}</td>
              <td className='table-column'>{add.deleted.toString()}</td>
            </tr>
          ))}
          </tbody>
        </table>
      </section>
    </section>
  )
}
export default ObjectsOverview;