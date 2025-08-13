import type { ModuleObj } from "../moduleobj"

type SideNavProps = {
  modules: ModuleObj[]
  onSelectModule: (module: ModuleObj) => void
  selectedModule?: ModuleObj | null
}

export default function SideNav({modules, onSelectModule, selectedModule}: SideNavProps) {
    return (
        <div className='bg-neutral-900 text=white w-64 px-8 py-4 rounded-r-md border-none text-gray-400'>
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