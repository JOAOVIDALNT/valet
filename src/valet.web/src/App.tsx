import { useEffect, useState } from "react"
import SideNav from "./components/sidenav"
import Module from "./components/module";
import type { ModuleObj } from "./moduleObj";
import { fetchModulesData } from "./modulesData";
import Search from "./components/search";


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
      <div className="relative flex-1 ml-64">
        <Search onSelectModule={setSelectedModule} />
        <Module module={selectedModule}/>
      </div>
    </div>
  )
}

export default App
