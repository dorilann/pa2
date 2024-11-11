
import {Navigate, Route, Routes} from "react-router-dom";
import { NavBar } from "../../components/common/header/NavBar";

const Routing = () => {
    return (
        <>
            <NavBar/>
            <div className="content">
                <Routes>
                    <Route path="/" element={<h1>penis</h1>}/>
                    <Route path="/login" element={<h1>penis</h1>}/>
                    <Route path="/profile" element={<></>}/>
                    <Route path="/admin" element={<></>}/>
                    <Route path="/product/:id" element={<></>}/>
                    <Route path="/search/:query" element={<></>}/>
                    <Route path="/search" element={<></>}/>
                    <Route
                        path="*"
                        element={<Navigate to="/" replace/>}
                    />

                </Routes>
            </div>
        </>

    );
}

export {Routing}