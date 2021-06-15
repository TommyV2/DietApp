import React from "react";
import {NavigationBar} from "../components/NavigationBar"

export class NoMatch extends React.Component {
    render(){
        return(
            <div>
                <NavigationBar type="main"/>
                No match (or not implemented yet)
            </div>
        );
    }
}