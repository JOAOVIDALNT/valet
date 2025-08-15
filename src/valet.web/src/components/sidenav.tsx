import type { ModuleObj } from "../moduleObj"

type SideNavProps = {
  modules: ModuleObj[]
  onSelectModule: (module: ModuleObj) => void
  selectedModule?: ModuleObj | null
}

export default function SideNav({modules, onSelectModule, selectedModule}: SideNavProps) {
    return (
        <div className='fixed top-0 left-0 h-screen bg-neutral-900 w-64 px-8 py-4 rounded-tr-md border-none text-gray-400 overflow-y-auto z-40'>
            <h1 className="pb-5">VALET</h1>
            <nav>
                {modules.map((module) => (
                    <a
                        key={module.module}
                        onClick={() => onSelectModule(module)}
                        className={
                          `block w-full text-left py-2.5 px-4 rounded transition cursor-pointer
                          ${selectedModule?.module === module.module
                            ? "text-white font-bold"
                            : "hover:text-white"}`
                        }
                    >
                        {module.module}
                    </a>
                ))}
            </nav>
        </div>
    )
}