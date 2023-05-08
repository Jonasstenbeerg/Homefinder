import {  useEffect, useState } from 'react';
import axios from 'axios';
import {useAuthHeader} from 'react-auth-kit'
import ObjectsNav from './objectsNav/ObjectsNav';
import ObjectsOverview from './ObjectsOverview';
import ManageObject from './ManageObject';
import styles from './Home.module.css'


const Home = () => {
  const authHeader = useAuthHeader()
  const [selectedobject, setSelectedObject] = useState(null)
  const [advertisements, setAdvertisements] = useState([])
  const [selectedNavItem, setSelectedNavItem] = useState("advertisements")
  const errorMessage = "something went wrong"

  
  const handleFetchAdvertisements = async () => {
    var res = await axios.get(`${process.env.REACT_APP_API_BASEURL}advertisements/list`)
    setAdvertisements(res.data)
  }

  const handleCreateAdvertisement = async (advertisement) => {
    try {
      const url = `${process.env.REACT_APP_API_BASEURL}advertisements`
    
      await axios.post(url,advertisement,{
        headers: {
          authorization: authHeader()
        }
      })

      await handleFetchAdvertisements()
    } catch (error) {
      //TODO: this should generate a custom error modal
      alert(errorMessage)
    }
  }

  const handleUpdateAdvertisement = async (advertisement) => {
    try {
      const url = `${process.env.REACT_APP_API_BASEURL}advertisements/${selectedobject.id}`
    
      await axios.put(url,advertisement,{
        headers: {
          authorization: authHeader()
        }
      })

      await handleFetchAdvertisements()
    } catch (error) {
      //TODO: this should generate a custom error modal
      alert(errorMessage)
    }
  }

  return (
    <section className={styles["home-container"]}>
      <nav>
        <ObjectsNav selctedNavITem={selectedNavItem} onSelectNavItem={setSelectedNavItem}></ObjectsNav>
      </nav>
      <article>
        {selectedNavItem === "advertisements" && (
          <ObjectsOverview advertisements={advertisements} onFetchAdvertisements={handleFetchAdvertisements} selectedRowIndex={advertisements.indexOf(selectedobject) < 0 ? 0 :advertisements.indexOf(selectedobject)} selectObject={setSelectedObject}></ObjectsOverview>
        )}
        {selectedNavItem === "users" && (
          <h1>Not implemented yet</h1>
        )}
      </article>
      <article>
        <ManageObject objectName={selectedNavItem.slice(0,selectedNavItem.length-1)} objectToManage={selectedobject || advertisements[0]} onCreateAdvertisement={handleCreateAdvertisement} onUpdateAdvertisement={handleUpdateAdvertisement}></ManageObject>
      </article>
    </section>
  )
}
export default Home;