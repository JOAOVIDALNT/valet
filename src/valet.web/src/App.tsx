import { useEffect, useState } from "react"
import SideNav from "./components/sidenav"
import Module from "./components/module";
import type { ModuleObj } from "./moduleObj";
import { fetchModulesData } from "./modulesData";
import Search from "./components/search";


function App() {
  
  const [modules, setModules] = useState<ModuleObj[]>([]);
  const [selectedModule, setSelectedModule] = useState<ModuleObj | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>("");

  useEffect(() => {
      fetchModulesData().then(data => {
          setModules(data);
          setSelectedModule(data[0]); 
      }).catch(error => {
          console.error("Error fetching modules data:", error);
      });
  }, [])
  
  function handleSelectModule(module: ModuleObj, term?: string) {
      setSelectedModule(module);
      setSearchTerm(term || "");
  }

  return (

    <div className='flex h-screen bg-black'>
      <SideNav modules={modules} onSelectModule={m => handleSelectModule(m)} selectedModule={selectedModule}/>
      <div className="relative flex-1 ml-64">
        <Search onSelectModule={(m, term) => handleSelectModule(m, term)} />
        <Module module={selectedModule} searchTerm={searchTerm}/>
      </div>
    </div>
  )
}

export default App
