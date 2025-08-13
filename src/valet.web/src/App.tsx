import { useEffect, useState } from "react"
import SideNav from "./components/sidenav"
import Module from "./components/module";
import type { ModuleObj } from "./moduleObj";
import { fetchModulesData } from "./modulesData";


function App() {
  
  const [modules, setModules] = useState<ModuleObj[]>([]);
  const [selectedModule, setSelectedModule] = useState<ModuleObj | null>(null);

  useEffect(() => {
      fetchModulesData().then(data => {
          setModules(data);
          setSelectedModule(data[0]); 
      }).catch(error => {
          console.error("Error fetching modules data:", error);
      });
  }, [])
  

  return (

    <div className='flex h-screen bg-black'>
      <SideNav modules={modules} onSelectModule={setSelectedModule} selectedModule={selectedModule}/>
      <Module module={selectedModule}/>
    </div>
  )
}

export default App
