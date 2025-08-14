
import type { ModuleObj } from "../moduleObj";

type ModuleProps = {
    module: ModuleObj | null
}

export default function Module({module}: ModuleProps) {

    return (
        <main className="text-white flex-1 bg-black p-6 w-full">
            <h1>{module?.title}</h1>
            <p className="break-words whitespace-pre-line">{module?.content}</p>
        </main>
    )
}