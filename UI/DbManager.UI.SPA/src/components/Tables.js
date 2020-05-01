import React from 'react';
import * as Constants from '../constants';

class Tables extends React.Component {

    constructor(props) {
        super(props);
        this.createTable = this.createTable.bind(this);
    }

    createTable(table) {
        fetch(`${Constants.SERVICEURL}/generatescripts/createtable`, {
            method: 'POST',
            mode: 'cors',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
            body: (JSON.stringify(table)),
        })
            .then(response => response.text())
            .then(data => {
                alert(data);
            })
            .catch(error => alert(error));
    }

    renderTableData() {
        return this.props.tables.map((table) => {
            return (
                <tr key={table.name}>
                    <td className="text-left" onClick={() => this.props.loadColumns(table)}>
                        {table.schema}.{table.name}</td>
                    <td>
                        <button onClick={() => this.createTable(table)} className="btn btn-secondary btn-sm">
                            Create
                        </button>
                    </td>
                </tr>
            )
        })
    }

    render() {
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th className="text-left" colspan="2">Tables</th>
                </tr>
                </thead>
                <tbody>
                {this.renderTableData()}
                </tbody>
            </table>
        )
    }
}

export default Tables;
