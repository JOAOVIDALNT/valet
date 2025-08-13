import type { ModuleObj } from "../moduleObj";
import Search from "./search";

type ModuleProps = {
    module: ModuleObj | null
}

export default function Module({module}: ModuleProps) {
    return (
        <main className="text-white flex-1 bg-black p-6">
            <Search />
            <h1>{module?.title}</h1>
            <p>{module?.content}</p>
        </main>
    )
}