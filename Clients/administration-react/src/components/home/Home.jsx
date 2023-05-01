import {  useState } from 'react';
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

  const handleFetchAdvertisements = async () => {
    var res = await axios.get(`${process.env.REACT_APP_API_BASEURL}advertisements/list`)
    setAdvertisements(res.data)
  }

  const handleCreateAdvertisement = async (advertisement) => {
    const url = `${process.env.REACT_APP_API_BASEURL}advertisements`
    
    await axios.post(url,advertisement,{
      headers: {
        authorization: authHeader()
      }
    })

    await handleFetchAdvertisements()
  }

  return (
    <section className={styles["home-container"]}>
      <nav>
        <ObjectsNav></ObjectsNav>
      </nav>
      <article>
        <ObjectsOverview advertisements={advertisements} onFetchAdvertisements={handleFetchAdvertisements} selectObject={setSelectedObject}></ObjectsOverview>
      </article>
      <article>
        <ManageObject objectToManage={selectedobject} onCreateAdvertisement={handleCreateAdvertisement}></ManageObject>
      </article>
    </section>
  )
}
export default Home;