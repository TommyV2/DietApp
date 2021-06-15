import React from "react";
import { NavigationBar } from "../../components/NavigationBar";
import { Tab, Tabs, Container } from "react-bootstrap";
import {ClientLogin} from "./ClientLogin";
import {ClientRegister} from "./ClientRegister";

export class ClientMainPage extends React.Component {
    render() {
        return (
            <div>
                <NavigationBar type="main" />
                <Container>
                    <Tabs defaultActiveKey="login" id="uncontrolled-tab-example">
                        <Tab eventKey="login" title="Zaloguj się">
                            <ClientLogin/>
                        </Tab>
                        <Tab eventKey="reister" title="Zarejestruj się">
                            <ClientRegister/>
                        </Tab>
                    </Tabs>
                </Container>
            </div>
        );
    }
}