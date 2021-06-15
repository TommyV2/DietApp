import React from "react";
import {NavigationBar} from "../../components/NavigationBar";
import { Tab, Tabs, Container } from "react-bootstrap";
import {SpecialistLogin} from "./SpecialistLogin";
import {SpecialistRegister} from "./SpecialistRegister";
import {getCookie} from "../../commons/getCookie";

export class SpecialistMainPage extends React.Component {

    componentWillMount(){
        if(getCookie("specialistId") != null){
            console.log("jest != null")
            window.location.href = '/specialist/dashboard';
        }
    }

    render(){
        return(
            <div>
                <NavigationBar type="main" />
                <Container>
                    <Tabs defaultActiveKey="login" id="uncontrolled-tab-example">
                        <Tab eventKey="login" title="Zaloguj się">
                            <SpecialistLogin/>
                        </Tab>
                        <Tab eventKey="reister" title="Zarejestruj się">
                            <SpecialistRegister/>
                        </Tab>
                    </Tabs>
                </Container>
            </div>
        );
    }
}